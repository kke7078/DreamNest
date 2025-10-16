using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/BlockItemList")]
    public class BlockItemList : BaseItemList
    {
        [SerializeField] private int listCount = 1;
        [SerializeField] private List<BlockItemData> itemDataList;
        public List<BlockItemData> ItemDataList => itemDataList;
        public int ListCount => listCount;

        public void SetListCount(int value)
        {
            listCount = value;
        }
        
    }

    [CustomEditor(typeof(BlockItemList))]

    public class BlockItemListEditor : BaseItemListEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("listCount"));

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
