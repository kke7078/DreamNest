using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class BlockItemData : BaseItemData
    {
        //아이템 구매 가격
        [SerializeField] private int itemBuyPrice;
        public int ItemBuyPrice => itemBuyPrice;
    }

    [CreateAssetMenu(menuName = "Data/BlockItemList")]
    public class BlockItemList : BaseItemListData
    {
        [SerializeField] private List<BlockItemData> itemDataList; //일반블록 아이템 데이터 리스트
        public List<BlockItemData> ItemDataList => itemDataList;

        public void OnEnable()
        {
            if (ItemDataList == null) itemDataList = new List<BlockItemData>();
            SetItemInfo(ItemDataList);    //아이디 설정
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BlockItemList))]
    public class BlockItemListEditor : BaseItemDataEditor{}
#endif
}
