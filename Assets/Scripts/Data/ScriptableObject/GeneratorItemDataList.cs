using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace mp
{
    [System.Serializable]
    public class GeneratorItemData : IItemData
    {
        //공통부 속성
        [SerializeField] string itemId;
        [SerializeField] string itemName;
        [SerializeField] int itemLevel;
        [SerializeField, TextArea] string itemDesc;
        [SerializeField] Sprite itemIcon;
        [SerializeField] int itemSellPrice;
        [SerializeField] int itemBuyPrice;

        public string ItemId => itemId;
        public string Itemname => itemName;
        public int ItemLevel => itemLevel;
        public string ItemDesc => itemDesc;
        public Sprite ItemIcon => itemIcon;
        public int ItemSellPrice => itemSellPrice;
        public int ItemBuyPrice => itemBuyPrice;

        //생성기 속성
        [SerializeField] private float cooldownTime; //재사용 대기 시간
        [SerializeField] private int maxCreateCount; //최대 생성 개수
        [SerializeField] private bool isDisposable;  //생성 후 파괴 여부

        public float CooldownTime => cooldownTime;
        public int MaxCreateCount => maxCreateCount;
        public bool IsDisposable => isDisposable;
    }

    [CreateAssetMenu(menuName = "Data/GeneratorItemDataList")]
    public class GeneratorItemDataList : ScriptableObject
    {
        //메인 카테고리
        [SerializeField] private MainItemType mainItemType;
        public MainItemType MainItemType => mainItemType;

        //서브 카테고리
        [SerializeField] private SingleItemType singleItemType;
        [SerializeField] private CraftItemType craftItemType;
        [SerializeField] private CurrencyItemType currencyItemType;

        //최대 레벨
        [SerializeField] private int maxLevel;
        public int MaxLevel => maxLevel;

        [SerializeField] private List<GeneratorItemData> generatorItemList;
        public List<GeneratorItemData> GeneratorItemList => generatorItemList;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GeneratorItemDataList))]
    public class GeneratorItemDataListEditor : Editor
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

            //항상 표시되어야 하는 속성
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxLevel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("generatorItemList"));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
