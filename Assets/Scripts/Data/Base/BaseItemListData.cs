using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    public class BaseItemListData : ScriptableObject
    {
        [SerializeField] private MainItemType mainItemType;         //메인 아이템 타입
        [SerializeField] private SingleItemType singleItemType;     //단일 아이템 타입
        [SerializeField] private CraftItemType craftItemType;       //합성 아이템 타입
        [SerializeField] private CurrencyItemType currencyItemType; //화폐 아이템 타입
        [SerializeField] private ItemGrade itemGrade;               //아이템 등급
        private int maxLevel;   //아이템의 최대 레벨

        public MainItemType MainItemType => mainItemType;
        public SingleItemType SingleItemType => singleItemType;
        public CraftItemType CraftItemType => craftItemType;
        public CurrencyItemType CurrencyItemType => currencyItemType;
        public ItemGrade ItemGrade => itemGrade;
        public int MaxLevel => maxLevel;


        protected void SetItemInfo<T>(List<T> itemListDatas) where T : BaseItemData
        {
            maxLevel = itemListDatas.Count;

            for (int i = 0; i < itemListDatas.Count; i++)
            {
                switch (MainItemType)
                {
                    case MainItemType.Single:
                        itemListDatas[i].ItemID = SingleItemType.ToString();
                        break;
                    case MainItemType.Craft:
                        itemListDatas[i].ItemID = CraftItemType.ToString();
                        break;
                    case MainItemType.Currency:
                        itemListDatas[i].ItemID = CurrencyItemType.ToString();
                        break;
                }

                itemListDatas[i].ItemID += ((((int)ItemGrade + 1) * 100) + i + 1).ToString("D3");
                itemListDatas[i].ItemLevel = i + 1;
            }
        }
    }

    public abstract class BaseItemDataEditor : Editor
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
            DrawAlwaysShownProperties();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawAlwaysShownProperties()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemGrade"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemDataList"));
        }
    }
}
