using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

namespace DreamNest
{
    [System.Serializable]
    public class GeneratorItemData : BaseItemData
    {
        [SerializeField] float maxGenerationCount;
        //[SerializeField] ItemPrice itemBuyPrice;

        public float MaxGenerationCount
        { 
            get => maxGenerationCount;
            set => maxGenerationCount = value;
        }

        //public ItemPrice ItemBuyPrice => itemBuyPrice;

        public override string SetItemInfo(BaseItemList list, int index)
        {
            string id = $"{list.ItemGeneratorType}{index:D3}";

            ItemID = id;
            ItemLevel = index;
            SetMaxGenerationCount(list as GeneratorItemList);


            return id;
        }
        private void SetMaxGenerationCount(GeneratorItemList list)
        {
            if (list != null)
            {
                switch (list.ItemGrade)
                {
                    case ItemGrade.Normal:
                        MaxGenerationCount = Mathf.Floor(ItemLevel * 2); break;
                    case ItemGrade.Rare:
                        MaxGenerationCount = Mathf.Floor(ItemLevel * 1.5f); break;
                }
            }
        }
    }
}
