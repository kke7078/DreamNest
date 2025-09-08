using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    //아이템에 공통으로 들어가는 속성, 기능 정리
    public abstract class BaseItemData : IItemBase
    {
        [SerializeField] private string itemID;                 //아이디
        [SerializeField] private int itemLevel;                 //레벨
        [SerializeField] private string itemName;               //이름
        [SerializeField, TextArea] private string itemDesc;     //설명
        [SerializeField] private Sprite itemIcon;               //아이콘
        [SerializeField] private int itemSellPrice;             //판매가격
        
        private bool isItemInfoSet = false;      //아이템 정보가 생성되었는지 여부

        public string ItemID 
        {
            get { return itemID; }
            set {
                isItemInfoSet = false;    //파일 리셋용 나중에 삭제

                if (!isItemInfoSet) 
                {
                    itemID = value;
                    isItemInfoSet = true;
                }
            }
        }

        public int ItemLevel
        {
            get { return itemLevel; }
            set 
            {
                isItemInfoSet = false;    //파일 리셋용 나중에 삭제
                if (!isItemInfoSet)
                {
                    itemLevel = value;
                    isItemInfoSet = true;
                }
            }
        }

        public string ItemName => itemName;

        public string ItemDesc => itemDesc;

        public Sprite ItemIcon => itemIcon;

        public int ItemSellPrice => itemSellPrice;
    }
}
