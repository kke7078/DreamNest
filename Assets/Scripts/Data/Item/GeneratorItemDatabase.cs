using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemDB")]
    public class GeneratorItemDatabase : ScriptableObject
    {
        [SerializeField] private List<GeneratorItemList> generatorItemList;
        public List<GeneratorItemList> GeneratorItemList => generatorItemList;

        private Dictionary<string, GeneratorItemData> itemDict;

        public void BuildDictionary()
        {
            if (itemDict != null) return;

            itemDict = new Dictionary<string, GeneratorItemData>();

            foreach (var list in generatorItemList) //생성기 아이템들의 리스트 순회
            {
                int index = 0;

                foreach (var item in list.ItemDataList)
                {
                    index++;
                    string id = item.SetItemInfo(list, item, index);

                    if (!itemDict.ContainsKey(id))
                    {
                        itemDict.Add(id, item);
                    }
                }
            }
        }

        public GeneratorItemData GetItemById(string id)
        {
            if (itemDict == null) BuildDictionary();
            itemDict.TryGetValue(id, out var item);

            return item;
        }
    }
}
