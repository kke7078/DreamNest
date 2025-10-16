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
        [SerializeField] private List<GeneratorItemData> itemDataList;

        public ItemGrade ItemGrade => itemGrade;
        public List<GeneratorItemData> ItemDataList => itemDataList;

        public void SetMaxGenerationCount()
        {
            foreach (var item in itemDataList)
            {
                switch (ItemGrade)
                {
                    case ItemGrade.Normal:
                        GetMaxGenerationCount(2, item); break;
                    case ItemGrade.Rare:
                        GetMaxGenerationCount(1.5f, item); break;
                }
            }
        }

        private void GetMaxGenerationCount(float multiple, GeneratorItemData data)
        {
            if(data != null) data.MaxGenerationCount = Mathf.Floor(data.ItemLevel * multiple);
        }

        [CustomEditor(typeof(GeneratorItemList))]
        public class GeneratorItemListEditor : BaseItemListEditor
        {
            public override void OnInspectorGUI()
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("itemGrade"));

                base.OnInspectorGUI();

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
