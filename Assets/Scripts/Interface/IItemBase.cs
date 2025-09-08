using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
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
        Dia,        //보석
        Ticket,     //티켓
    }

    public enum ItemGrade 
    {
        Normal,
        Rare
    }

    public interface IItemBase  // 아이템에 공통으로 들어가는 속성 정리
    {
        string ItemID { get; set; }
        int ItemLevel { get; set; }
        string ItemName { get; }
        string ItemDesc { get; }
        Sprite ItemIcon { get; }
        int ItemSellPrice { get; }
    }
}
