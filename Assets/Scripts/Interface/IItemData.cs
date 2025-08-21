using System.Collections;
using System.Collections.Generic;
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
        ex01,
        ex02,
    }

    public enum CurrencyItemType
    {
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

    public interface IItemData
    {
        string ItemId { get; }          //아이디
        string Itemname { get; }        //이름
        int ItemLevel { get; }          //레벨
        string ItemDesc { get; }        //설명
        Sprite ItemIcon { get; }        //아이콘
        int ItemSellPrice { get; }      //판매 가격
        int ItemBuyPrice { get; }       //구매 가격
    }
}
