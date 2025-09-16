using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    public enum ItemMainType    //아이템의 메인 카테고리
    { 
        Block,
        Generator,
        Currency
    }

    public enum ItemElementType     //일반블럭, 생성기에서 사용되는 카테고리
    {
        Plt,    //식물계
        Glw,    //별, 빛
        Fab,    //천, 인형, 직물
        Gem,    //수정, 보석 결정
        Alc,    //물약, 혼합물, 연금
        Sha,    //어둠, 그림자
        Sky,    //구름, 날개, 비행
        Aqa,    //물, 얼음
        Fir,    //불
        Mec,    //기계, 태엽
        Mbs,    //환상종, 펫
    }

    public enum ItemCurrencyType    //재화에서 사용되는 카테고리
    {
        Energy,     //에너지
        Gold,       //골드
        Dia,        //보석
    }

    public class BaseItemList : ScriptableObject
    {
        [SerializeField] private ItemMainType itemMainType;
        [SerializeField] private ItemElementType itemElementType;
        [SerializeField] private ItemCurrencyType currencyType;

        public ItemMainType ItemMainType => itemMainType;
        public ItemElementType ItemElementType => itemElementType;
        public ItemCurrencyType CurrencyType => currencyType;

        //리스트에 들어있는 아이템에 정보 입력
        public void SetItemInfo<T>(List<T> itemList) where T : BaseItemData
        {
            foreach (var item in itemList)
            {
                if ((item is BlockItemData block))
                {
                    Debug.Log("이 리스트는 block 파일이야");
                }
            }
        }
    }

    [CustomEditor(typeof(BaseItemList), true)]
    public class BaseITemListEditor : Editor
    {
        SerializedProperty itemMainTypeProp;
        SerializedProperty itemElementTypeProp;
        SerializedProperty currencyTypeProp;
        SerializedProperty itemLists;

        void OnEnable()
        {
            itemMainTypeProp = serializedObject.FindProperty("itemMainType");
            itemElementTypeProp = serializedObject.FindProperty("itemElementType");
            currencyTypeProp = serializedObject.FindProperty("currencyType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(itemMainTypeProp);

            ItemMainType mainType = (ItemMainType)itemMainTypeProp.enumValueIndex;
            switch (mainType)
            {
                case ItemMainType.Block:
                case ItemMainType.Generator:
                    EditorGUILayout.PropertyField(itemElementTypeProp);
                    break;
                case ItemMainType.Currency:
                    EditorGUILayout.PropertyField(currencyTypeProp);
                    break;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemDataList"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
