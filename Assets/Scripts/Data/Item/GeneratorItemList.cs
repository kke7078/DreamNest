using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemList")]
    public class GeneratorItemList : BaseItemList
    {
        [SerializeField] private ItemGrade itemGrade;
        [SerializeField] private int itemCooltime;
        [SerializeField] private List<GeneratorItemData> itemDataList;

        public ItemGrade ItemGrade => itemGrade;
        public int ItemCooltime => itemCooltime;
        public List<GeneratorItemData> ItemDataList => itemDataList;
    }

    [CustomEditor(typeof(GeneratorItemList))]
    public class GeneratorItemListEditor : BaseItemListEditor
    {
        private SerializedProperty itemGradeProp;
        private SerializedProperty itemCooltimeProp;
        private SerializedProperty itemDataListProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            itemGradeProp = serializedObject.FindProperty("itemGrade");
            itemCooltimeProp = serializedObject.FindProperty("itemCooltime");
            itemDataListProp = serializedObject.FindProperty("itemDataList");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(itemGradeProp);
            EditorGUILayout.PropertyField(itemCooltimeProp);
            EditorGUILayout.PropertyField(itemDataListProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
