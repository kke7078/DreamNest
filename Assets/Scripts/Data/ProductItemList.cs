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

    [System.Serializable]
    public class ProductItemData
    {
        public Sprite icon;
        public string itemName;
        [TextArea] public string description;
    }

    [CreateAssetMenu(menuName = "Data/ProductItemList")]
    public class ProductItemList : ScriptableObject //아이템의 정보를 담고 있는 클래스
    {
        [SerializeField] private ItemCategory category;
        public ItemCategory Category => category; //아이템의 종류를 반환하는 프로퍼티

        public List<ProductItemData> items = new List<ProductItemData>(); //아이템 리스트
    }
}