using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class BlockItemData : BaseItemData
    {
        [SerializeField] ItemPrice itemSellPrice;
        [SerializeField] ItemPrice itemBuyPrice;
        public ItemPrice ItemSellPrice => itemSellPrice;
        public ItemPrice ItemBuyPrice => itemBuyPrice;
    }
}
