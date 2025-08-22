using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace mp
{
    public enum MainItemType
    {
        Single,      //단일 아이템
        Craft,       //합성 아이템
        Currency,    //화폐 아이템
    }

    public enum SingleItemType
    {
        None,   //없음
        Plt,    //식물계
        Glw,    //별, 빛
        Fab,    //천, 인형, 직물
        Gem,    //수정, 보석 결정
        Alc,    //물약, 혼합물, 연금
        Sha,    //어둠, 그림자
        Sky,    //구름, 날개, 비행
        Aqa,    //물, 얼음
        Fir,    //불
        Mec,    //기계, 태엽
        Mbs,    //환상종, 펫
    }

    public enum CraftItemType
    {
        //추후 추가 예정
        None,   //없음
        ex01,
        ex02,
    }

    public enum CurrencyItemType
    {
        None,       //없음
        Energy,     //에너지
        Gold,       //골드
        Gem,        //보석
        Ticket,     //티켓
    }

    public enum ItemGrade
    {
        Normal,    //일반
        Rare,      //희귀
    }

    // 아이템 공통 데이터 클래스
    [System.Serializable]
    public abstract class ItemCommonData
    {
        [Header("공통 아이템 속성")]
        [SerializeField] protected string itemId;               //아이디
        [SerializeField] protected string itemName;             //이름
        [SerializeField] protected int itemLevel;               //레벨
        [SerializeField, TextArea] protected string itemDesc;   //설명
        [SerializeField] protected Sprite itemIcon;             //아이콘
        [SerializeField] protected int itemSellPrice;           //판매 가격
        [SerializeField] protected int itemBuyPrice;            //구매 가격

        public string ItemId => itemId;
        public string ItemName => itemName;
        public int ItemLevel => itemLevel;
        public string ItemDesc => itemDesc;
        public Sprite ItemIcon => itemIcon;
        public int ItemSellPrice => itemSellPrice;
        public int ItemBuyPrice => itemBuyPrice;
    }
}
