using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    public enum ItemCurrencyType
    { 
        Gold,
        DIa,
        Energy,
    }

    [System.Serializable]
    public class ItemPrice
    {
        [SerializeField] private ItemCurrencyType currencyType;
        [SerializeField] private int price;

        public ItemCurrencyType ItemCurrencyType => currencyType;
        public int Price => price;
    }

    public class BaseItemData : IItemBaseData
    {
        [SerializeField] private string itemID;
        [SerializeField] private int itemLevel;
        //[SerializeField] string itemName;
        //[SerializeField, TextArea] string itemDesc;

        public string ItemID { get; private set; }
        public int ItemLevel { get; private set; }

        public void SetItemId(string id)
        {
            itemID = id;        //필드에 값 넣기
            ItemID = itemID;    //프로퍼티에 동기화
        }

        public void SetItemLevel(int level)
        {
            itemLevel = level;
            ItemLevel = itemLevel;
        }

        //public string ItemName => throw new System.NotImplementedException();

        //public string ItemDesc => throw new System.NotImplementedException();

        //아이템 정보 세팅 → 아이디값 각 DB에 넘겨서 Dictionary 세팅
        public string SetItemInfo(BaseItemList list, BaseItemData data, int index)
        {
            string id = "";

            if (list is BlockItemList blockList)
            {
                id = $"{list.ItemBlockType}{(100 * blockList.ListCount + index):D3}";

                if (GameManager.Instance.BlockItemDB.GetItemById(id) == null)
                {
                    SetItemId(id);
                    SetItemLevel(index);

                    return id;
                }
                else
                {
                    blockList.SetListCount(blockList.ListCount + 1);
                    SetItemInfo(list, data, index); //다시 돌리기
                }
            }
            else if (list is GeneratorItemList generatorLIst)
            {
                id = $"{list.ItemGeneratorType}{index:D3}";

                SetItemId(id);
                SetItemLevel(index);

                generatorLIst.SetMaxGenerationCount();

                return id;
            }

            return "";
        }
    }


#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ItemPrice))]
    public class ItemPriceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 라벨 그리기
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Rect 나누기
            float width = position.width;
            var currencyRect = new Rect(position.x, position.y, width * 0.4f, position.height);
            var priceRect = new Rect(position.x + width * 0.42f, position.y, width * 0.58f, position.height);

            // 필드 찾기
            var currencyProp = property.FindPropertyRelative("currencyType");
            var priceProp = property.FindPropertyRelative("price");

            // 그리기
            EditorGUI.PropertyField(currencyRect, currencyProp, GUIContent.none);
            EditorGUI.PropertyField(priceRect, priceProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
#endif
}
