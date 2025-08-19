using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public enum ItemCategory //아이템의 종류
    {
        Dog,
        Cat,
        Rabbit
    }

    public interface IItemData  //아이템 공통 데이터 (프로덕트, 생성기 공통으로 들어갈 데이터)
    {
        Sprite Icon { get; }
        string Name { get; }
        string Description { get; }
        
    }
}
