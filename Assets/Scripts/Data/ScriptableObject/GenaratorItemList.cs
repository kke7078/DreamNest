using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public enum  ItemGrade
    {
        Normal,
        Rare,
    }

    public class GenaratorItemData
    {
        [SerializeField] Sprite icon; //아이템의 아이콘
        [SerializeField] string itemName; //아이템의 이름
        [SerializeField][TextArea] string description; //아이템의 설명
        public Sprite Icon => icon;
        public string Name => itemName;
        public string Description => description;
    }

    [CreateAssetMenu(menuName = "Data/GeneratorItemList")]
    public class GenaratorItemList : ScriptableObject
    {
        [SerializeField] private ItemCategory category; //아이템의 종류
        [SerializeField] private ItemGrade grade; //아이템의 등급
        public ItemCategory Category => category;
        public ItemGrade Grade => grade;



        public List<ProductItemData> items = new List<ProductItemData>(); //아이템 리스트
    }
}
