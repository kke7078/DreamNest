using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    [System.Serializable]
    public class ProductItemData
    {
        [SerializeField] Sprite icon; //아이템의 아이콘
        [SerializeField] string itemName; //아이템의 이름
        [SerializeField][TextArea] string description; //아이템의 설명

        public Sprite Icon => icon;
        public string Name => itemName;
        public string Description => description;
    }

    [CreateAssetMenu(menuName = "Data/ProductItemList")]
    public class ProductItemList : ScriptableObject //아이템의 정보를 담고 있는 클래스
    {
        [SerializeField] private ItemCategory category; //아이템의 종류
        public ItemCategory Category => category;

        public List<ProductItemData> items = new List<ProductItemData>(); //아이템 리스트
    }
}