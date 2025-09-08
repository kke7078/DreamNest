using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class BlockItemData : BaseItemData
    {
        //아이템 구매 가격
        [SerializeField] private int itemBuyPrice;
        public int ItemBuyPrice => itemBuyPrice;
    }

    [CreateAssetMenu(menuName = "Data/BlockItemList")]
    public class BlockItemList : ScriptableObject
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

        [SerializeField] private List<BlockItemData> itemDataList; //일반블록 아이템 데이터 리스트
        public List<BlockItemData> ItemDataList => itemDataList;

        public void OnEnable()
        {
            if (ItemDataList == null) itemDataList = new List<BlockItemData>();
            maxLevel = ItemDataList.Count;

            SetItemInfo();    //아이디 설정
        }

        private void SetItemInfo()
        {
            for (int i = 0; i < ItemDataList.Count; i++)
            {
                switch (MainItemType)
                {
                    case MainItemType.Single:
                        ItemDataList[i].ItemID = SingleItemType.ToString();
                        break;
                    case MainItemType.Craft:
                        ItemDataList[i].ItemID = CraftItemType.ToString();
                        break;
                    case MainItemType.Currency:
                        ItemDataList[i].ItemID = CurrencyItemType.ToString();
                        break;
                }

                ItemDataList[i].ItemID += ((((int)ItemGrade + 1) * 100) + i + 1).ToString("D3");
                ItemDataList[i].ItemLevel = i + 1;
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BlockItemList))]
    public class BlockItemListEditor : Editor
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemDataList"));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
