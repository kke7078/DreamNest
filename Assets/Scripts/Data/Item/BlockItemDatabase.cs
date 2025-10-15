using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using static UnityEditor.Progress;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/BlockItemDB")]
    public class BlockItemDatabase : ScriptableObject
    {
        [SerializeField] private List<BlockItemList> blockItemList;
        [SerializeField] private Dictionary<string, BaseItemData> itemDict;

        public List<BlockItemList> BlockItemList => blockItemList;
        public Dictionary<string, BaseItemData> ItemDict => itemDict;
        

        public void BuildDictionary()
        {
            if (itemDict != null) return;
            
            itemDict = new Dictionary<string, BaseItemData>();

            foreach (var list in blockItemList) //블록아이템들의 리스트 순회
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

            return item;
        }
    }
}
