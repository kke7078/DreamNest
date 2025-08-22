using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace mp
{
    [System.Serializable]
    public class ProductItemData : ItemCommonData
    {
        
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

        //등급
        [SerializeField] private ItemGrade itemGrade;
        public ItemGrade ItemGrade => itemGrade;

        //최대 레벨
        [SerializeField] private int maxLevel;
        public int MaxLevel => maxLevel;

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

            //EditorGUI.BeginChangeCheck, EditorGUI.EndChangeCheck : 두 함수 사이에 들어있는 GUI의 값이 바뀌었는지 체크
            EditorGUI.BeginChangeCheck();   
            EditorGUILayout.PropertyField(mainItemTypeProp);
            if (EditorGUI.EndChangeCheck())
            {
                // 모든 서브 타입을 None으로 초기화
                serializedObject.FindProperty("singleItemType").enumValueIndex = (int)SingleItemType.None;
                serializedObject.FindProperty("craftItemType").enumValueIndex = (int)CraftItemType.None;
                serializedObject.FindProperty("currencyItemType").enumValueIndex = (int)CurrencyItemType.None;
            }

            switch ((MainItemType)mainItemTypeProp.enumValueIndex)
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

            //항상 표시되어야 하는 속성
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemGrade"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxLevel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("productItemList"));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
