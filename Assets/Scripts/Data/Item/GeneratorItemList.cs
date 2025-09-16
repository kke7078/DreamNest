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
            if (itemDataList == null) itemDataList = new List<GeneratorItemData>();
            SetItemInfo(itemDataList);
        }
    }
}
