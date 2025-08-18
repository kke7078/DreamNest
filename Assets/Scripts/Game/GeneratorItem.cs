using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public class GeneratorItem : MonoBehaviour  //아이템 생성기 클래스
    {
        private ProductItemDataBase productItemDataBase; //프로덕트 아이템 데이터베이스
        
        [SerializeField] private ItemCategory category;   //생성기의 아이템 카테고리
    }
}
