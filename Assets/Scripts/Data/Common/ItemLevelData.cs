using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class  PriceData
    {
        [SerializeField] private CurrencyItemType currency;
        [SerializeField] private int price;

        public CurrencyItemType Currency => currency;
        public int Price => price;
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField] private int level;
        [SerializeField] private int cooltime;
        [SerializeField] private PriceData CellPriceData;

        public int Level => level;
        public int Cooltime => cooltime;
        public PriceData CellPrice => CellPriceData;
    }

    [CreateAssetMenu(menuName ="Data/Common/ItemLevelData")]
    public class ItemLevelData : ScriptableObject
    {
        [SerializeField] private List<LevelData> levelDataList;
        public List<LevelData> LevelDataList => levelDataList;
    }
    




#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PriceData))]
    public class PriceDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // 줄 시작 위치
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);

            // 두 필드 나눌 비율
            float halfWidth = contentPosition.width / 2f;

            // currency
            var currencyProp = property.FindPropertyRelative("currency");
            var priceProp = property.FindPropertyRelative("price");

            // 필드 위치 조정
            Rect currencyRect = new Rect(contentPosition.x, contentPosition.y, halfWidth - 5, contentPosition.height);
            Rect priceRect = new Rect(contentPosition.x + halfWidth + 5, contentPosition.y, halfWidth - 5, contentPosition.height);

            EditorGUI.PropertyField(currencyRect, currencyProp, GUIContent.none);
            EditorGUI.PropertyField(priceRect, priceProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
#endif
}
