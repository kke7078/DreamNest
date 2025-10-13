using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemList")]
    public class GeneratorItemList : BaseItemList
    {
        [SerializeField] private List<BlockItemList> spawnItemList;

        [SerializeField] private List<GeneratorItemData> itemDataList;
        public List<GeneratorItemData> ItemDataList => itemDataList;

        //리스트 아이템 정보 입력
        public void OnEnable()
        {
            if (ItemDataList == null) itemDataList = new List<GeneratorItemData>();
            SetItemInfo(itemDataList);
        }


        public override void SetItemInfo<T>(List<T> itemList)
        {
            base.SetItemInfo(itemList);

            switch (ItemGrade)
            {
                case ItemGrade.Normal:
                    SetMaxGenerationCount(2, itemList); break;
                case ItemGrade.Rare:
                    SetMaxGenerationCount(1.5f, itemList); break;
            }
        }

        private void SetMaxGenerationCount<T>(float multiple, List<T> itemList)
        {
            foreach (var item in itemList)
            {
                GeneratorItemData generatorItem = item as GeneratorItemData;

                if (generatorItem != null)
                {
                    generatorItem.MaxGenerationCount = Mathf.Floor(generatorItem.ItemLevel * multiple);
                }
            }
        }

        public void InitializeSpawnList()
        {
            Debug.Log(GameManager.Instance.BlockItemDB.BlockItemList.Count);
        }

        [CustomEditor(typeof(GeneratorItemList))]
        public class GeneratorItemListEditor : BaseItemListEditor 
        {
            SerializedProperty spawnItemList;

            protected override void OnEnable()
            {
                spawnItemList = serializedObject.FindProperty("spawnItemList");
                base.OnEnable();
            }

            public override void OnInspectorGUI()
            {
                EditorGUILayout.PropertyField(spawnItemList);
                serializedObject.ApplyModifiedProperties();
                base.OnInspectorGUI();
            }
        }
    }
}
