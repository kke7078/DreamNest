using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class GeneratorItemData : BaseItemData
    {
        [SerializeField] float maxGenerationCount;
        //[SerializeField] ItemPrice itemBuyPrice;

        public float MaxGenerationCount
        {
            get { return maxGenerationCount; }
            set { maxGenerationCount = value; }
        }
        //public ItemPrice ItemBuyPrice => itemBuyPrice;
    }
}
