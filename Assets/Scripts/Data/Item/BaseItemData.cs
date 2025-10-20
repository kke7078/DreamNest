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

    public abstract class BaseItemData : IItemBaseData
    {
        [SerializeField] private string itemID;
        [SerializeField] private int itemLevel;
        //[SerializeField] string itemName;
        //[SerializeField, TextArea] string itemDesc;

        public string ItemID
        {
            get => itemID;
            set => itemID = value;
        }
        public int ItemLevel
        {
            get => itemLevel;
            set => itemLevel = value;
        }

        //public string ItemName => throw new System.NotImplementedException();

        //public string ItemDesc => throw new System.NotImplementedException();

        //아이템 정보 세팅 → 아이디값 각 DB에 넘겨서 Dictionary 세팅
        public abstract string SetItemInfo(BaseItemList list, int index);
    }
}
