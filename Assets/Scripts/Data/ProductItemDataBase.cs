using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace mp
{
    [CreateAssetMenu(menuName = "Data/ProductItemDataBase")]
    public class ProductItemDataBase : ScriptableObject //프로덕트 아이템 데이터베이스 클래스
    {
        public List<ProductItemList> allItemList = new List<ProductItemList>();

        public List<ProductItemData> GetItemsByCategory(ItemCategory category)
        {
            ProductItemList list = allItemList.Find(itemList => itemList.Category == category);
            if (list != null) return list.items;
            
            return new List<ProductItemData>();
        }
    }
}
