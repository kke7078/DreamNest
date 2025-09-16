using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class GeneratorItemData : BaseItemData
    {
        [SerializeField] ItemPrice itemBuyPrice;
        public ItemPrice ItemBuyPrice => itemBuyPrice;
    }
}
