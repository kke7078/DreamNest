using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        [SerializeField] int itemSellPrice;
        [SerializeField] int itemBuyPrice;

        //공통부 속성
        public string ItemId => itemId;
        public string Itemname => itemName;
        public int ItemLevel => itemLevel;
        public string ItemDesc => itemDesc;
        public Sprite ItemIcon => itemIcon;
        public int ItemSellPrice => itemSellPrice;
        public int ItemBuyPrice => itemBuyPrice;
    }

    [CreateAssetMenu(menuName = "Data/ProductItemDataList")]
    public class ProductItemDataList : ScriptableObject
    {
        //메인 카테고리
        [SerializeField] private MainItemType mainItemType;
        public MainItemType MainItemType => mainItemType;

        //서브 카테고리
        [SerializeField] private SingleItemType singleItemType;
        [SerializeField] private CraftItemType craftItemType;
        [SerializeField] private CurrencyItemType currencyItemType;

        [SerializeField] private ItemGrade itemGrade;
        public ItemGrade ItemGrade => itemGrade;

        [SerializeField] private List<ProductItemData> productItemList;
        public List<ProductItemData> ProductItemList => productItemList;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ProductItemDataList))]
    public class ProductItemDataListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //메인 카테고리 표시
            var mainItemTypeProp = serializedObject.FindProperty("mainItemType");
            EditorGUILayout.PropertyField(mainItemTypeProp);

            MainItemType mainItemType = (MainItemType)mainItemTypeProp.enumValueIndex;

            switch (mainItemType)
            {
                case MainItemType.Single:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("singleItemType"));
                    break;
                case MainItemType.Craft:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("craftItemType"));
                    break;
                case MainItemType.Currency:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("currencyItemType"));
                    break;
            }

            // 아이템 등급과 리스트는 항상 표시
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemGrade"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("productItemList"));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
