using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemDB")]
    public class GeneratorItemDatabase : ScriptableObject
    {
        private static GeneratorItemDatabase instance;
        public static GeneratorItemDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<GeneratorItemDatabase>("GeneratorItemDB");
                }

                return instance;
            }
        }

        [SerializeField] private List<GeneratorItemList> generatorItemList;
        public List<GeneratorItemList> GeneratorItemList => generatorItemList;

        private Dictionary<string, BaseItemData> itemDict;

        public void BuildDictionary()
        {
            if (itemDict != null) return;

            itemDict = new Dictionary<string, BaseItemData>();

            foreach (var list in generatorItemList) //생성기 아이템들의 리스트 순회
            {
                foreach (var item in list.ItemDataList)
                {
                    BaseItemData baseData = item as BaseItemData;
                    if (baseData != null)
                    {
                        if (!itemDict.ContainsKey(baseData.ItemID))
                        {
                            itemDict.Add(baseData.ItemID, baseData);
                        }
                    }
                }
            }
        }

        public BaseItemData GetItemById(string id)
        {
            if (itemDict == null) BuildDictionary();
            itemDict.TryGetValue(id, out var item);

            Debug.Log(item);

            return item;
        }
    }
}
