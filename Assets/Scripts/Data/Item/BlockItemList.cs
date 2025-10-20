using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/BlockItemList")]
    public class BlockItemList : BaseItemList
    {
        [SerializeField] private int listCount;
        [SerializeField] private List<BlockItemData> itemDataList;
        public List<BlockItemData> ItemDataList => itemDataList;
        public int ListCount 
        {
            get => listCount;
            set => listCount = value;
        }
    }

    [CustomEditor(typeof(BlockItemList))]
    public class BlockItemListEditor : BaseItemListEditor
    {
        private SerializedProperty itemDataListProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            itemDataListProp = serializedObject.FindProperty("itemDataList");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(itemDataListProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
