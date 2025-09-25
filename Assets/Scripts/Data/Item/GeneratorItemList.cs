using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemList")]
    public class GeneratorItemList : BaseItemList
    {
        [SerializeField] private List<GeneratorItemData> itemDataList;
        public List<GeneratorItemData> ItemDataList => itemDataList;

        //리스트 아이템 정보 입력
        public void OnEnable()
        {
            if(ItemDataList == null) itemDataList = new List<GeneratorItemData>();
            SetItemInfo(itemDataList);
        }

        

        public override void SetItemInfo<T>(List<T> itemList)
        {
            base.SetItemInfo(itemList);

            switch (ItemGrade)
            {
                case ItemGrade.Normal:
                    SetMaxGenerationCount(2, itemList); break;
                case ItemGrade.Rare:
                    SetMaxGenerationCount(1.5f, itemList); break;
            }
        }

        private void SetMaxGenerationCount<T>(float multiple, List<T> itemList)
        {
            foreach (var item in itemList)
            {
                GeneratorItemData generatorItem = item as GeneratorItemData;

                if (generatorItem != null)
                {
                    generatorItem.MaxGenerationCount = Mathf.Floor(generatorItem.ItemLevel * multiple);
                }
            }
        }
    }
}
