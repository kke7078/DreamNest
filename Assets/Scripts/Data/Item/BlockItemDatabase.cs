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
        [SerializeField] private Dictionary<string, BlockItemData> itemDict;

        public List<BlockItemList> BlockItemList => blockItemList;
        public Dictionary<string, BlockItemData> ItemDict => itemDict;
        

        public void BuildDictionary()
        {
            if (itemDict != null) return;
            
            itemDict = new Dictionary<string, BlockItemData>();

            foreach (var list in blockItemList) //블록아이템들의 리스트 순회
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

        public BlockItemData GetItemById(string id)
        {
            if (itemDict == null) BuildDictionary();
            itemDict.TryGetValue(id, out var item);

            return item;
        }
    }
}
