using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class BlockItemData : BaseItemData
    {
        public override string SetItemInfo(BaseItemList list, int index)
        {
            BlockItemList blockList = list as BlockItemList;
            string id = $"{list.ItemBlockType}{(100 * blockList.ListCount + index):D3}";

            if (GameManager.Instance.BlockItemDB.GetItemById(id) == null)
            {
                ItemID = id;
                ItemLevel = index;

                return id;
            }
            else
            {
                blockList.ListCount++;
                SetItemInfo(list, index); //다시 돌리기
            }

            return id;
        }
        //[SerializeField] ItemPrice itemSellPrice;
        //[SerializeField] ItemPrice itemBuyPrice;
        //public ItemPrice ItemSellPrice => itemSellPrice;
        //public ItemPrice ItemBuyPrice => itemBuyPrice;
    }
}
