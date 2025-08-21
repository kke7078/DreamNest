using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    [System.Serializable]
    public class ProductItemData : IItemData
    {
        [SerializeField] string itemId;               
        [SerializeField] string itemName;          
        [SerializeField] int itemLevel;            
        [SerializeField, TextArea] string itemDesc;
        [SerializeField] Sprite itemIcon;
        [SerializeField] bool isNotMergeable;
        [SerializeField] int itemSellPrice;
        [SerializeField] int itemBuyPrice;


        public string ItemId => itemId;
        public string Itemname => itemName;
        public int ItemLevel => itemLevel;
        public string ItemDesc => itemDesc;
        public Sprite ItemIcon => itemIcon;
        public bool IsNotMergeable => isNotMergeable;
        public int ItemSellPrice => itemSellPrice;
        public int ItemBuyPrice => itemBuyPrice;
    }

    [CreateAssetMenu(menuName = "Data/ProductItemDataList")]
    public class ProductItemDataList : ScriptableObject
    {
        [SerializeField] private ItemCategory itemCategory;
        public ItemCategory ItemCategory => itemCategory;

        [SerializeField] private List<ProductItemData> productItemList;
        public List<ProductItemData> ProductItemList => productItemList;
    }
}
