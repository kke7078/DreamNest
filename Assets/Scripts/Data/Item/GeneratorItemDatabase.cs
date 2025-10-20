using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    public class GeneratorItemInfo
    {
        public GeneratorItemList List { get; private set; }
        public GeneratorItemData Data { get; private set; }

        public GeneratorItemInfo(GeneratorItemList list, GeneratorItemData data)
        {
            this.List = list;
            this.Data = data;
        }
    }

    [CreateAssetMenu(menuName = "Data/GeneratorItemDB")]
    public class GeneratorItemDatabase : ScriptableObject
    {
        [SerializeField] private List<GeneratorItemList> generatorItemList;
        public List<GeneratorItemList> GeneratorItemList => generatorItemList;

        private Dictionary<string, GeneratorItemInfo> itemDict;

        public void BuildDictionary()
        {
            if (itemDict != null) return;

            itemDict = new Dictionary<string, GeneratorItemInfo>();

            foreach (var list in generatorItemList) //생성기 아이템들의 리스트 순회
            {
                int index = 0;

                foreach (var item in list.ItemDataList)
                {
                    index++;
                    string id = item.SetItemInfo(list, index);

                    if (!itemDict.ContainsKey(id))
                    {
                        GeneratorItemInfo info = new GeneratorItemInfo(list, item);
                        itemDict.Add(id, info);
                    }
                }
            }
        }

        public GeneratorItemInfo GetItemById(string id)
        {
            if (itemDict == null) BuildDictionary();
            itemDict.TryGetValue(id, out var item);

            return item;
        }
    }
}
