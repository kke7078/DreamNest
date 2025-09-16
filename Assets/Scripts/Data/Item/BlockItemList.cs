using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/BlockItemList")]
    public class BlockItemList : BaseItemList
    {
        [SerializeField] private ItemElementType itemElementType;
        [SerializeField] private List<BlockItemData> itemDataList;

        public ItemElementType ItemElementType  => itemElementType;
        public List<BlockItemData> ItemDataList => itemDataList;


        //리스트 아이템 정보 입력
        public void OnEnable()
        {
            if(itemDataList == null) itemDataList = new List<BlockItemData>();
            SetItemInfo(itemDataList);
        }
    }
}
