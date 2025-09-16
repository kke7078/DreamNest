using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace DreamNest
{
    public enum ItemMainType    //아이템의 메인 카테고리
    { 
        Block,
        Generator,
    }

    public enum ItemBlockType     //일반블럭에서 사용되는 카테고리
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
        Energy,     //에너지
        Gold,       //골드
        Dia,        //보석
    }

    public enum ItemGeneratorType    //생성기에서 사용되는 카테고리
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
        Prm,    //프리미엄 (재화)
    }

    public enum ItemGrade       //아이템 등급
    { 
        Normal,
        Rare
    }

    public class BaseItemList : ScriptableObject
    {
        [SerializeField] private ItemMainType itemMainType;
        [SerializeField] private ItemBlockType itemBlockType;
        [SerializeField] private ItemGeneratorType itemGeneratorType;
        [SerializeField] private ItemGrade itemGrade;

        public ItemMainType ItemMainType => itemMainType;
        public ItemBlockType ItemBlockType => itemBlockType;
        public ItemGeneratorType ItemGeneratorType => itemGeneratorType;
        public ItemGrade ItemGrade => itemGrade;

        //리스트에 들어있는 아이템에 정보 입력
        public void SetItemInfo<T>(List<T> itemList) where T : BaseItemData
        {
            string id = "";
            for (int i = 0; i < itemList.Count; i++)
            {
                int indexValue = i + 1;

                if (itemList[i] is GeneratorItemData) id = $"{ItemGeneratorType}{indexValue:D3}";
                else
                {
                    int gradeValue = (int)ItemGrade + 1;
                    int totalValue = gradeValue * 100 + indexValue;

                    id = $"{ItemBlockType}{totalValue:D3}";
                }
                
                itemList[i].SetItemId(id);
            }
        }
    }

    [CustomEditor(typeof(BaseItemList), true)]
    public class BaseITemListEditor : Editor
    {
        SerializedProperty itemMainTypeProp;
        SerializedProperty itemBlockTypeProp;
        SerializedProperty itemGeneratorTypeProp;
        SerializedProperty itemLists;

        void OnEnable()
        {
            itemMainTypeProp = serializedObject.FindProperty("itemMainType");
            itemBlockTypeProp = serializedObject.FindProperty("itemBlockType");
            itemGeneratorTypeProp = serializedObject.FindProperty("itemGeneratorType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(itemMainTypeProp);

            ItemMainType mainType = (ItemMainType)itemMainTypeProp.enumValueIndex;
            switch (mainType)
            {
                case ItemMainType.Block:
                
                    EditorGUILayout.PropertyField(itemBlockTypeProp);
                    break;
                case ItemMainType.Generator:
                    EditorGUILayout.PropertyField(itemGeneratorTypeProp);
                    break;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemGrade"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemDataList"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
