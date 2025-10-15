using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemList")]
    public class GeneratorItemList : BaseItemList
    {
        [SerializeField] private List<BaseItemList> spawnItemList;
        [SerializeField] private List<GeneratorItemData> itemDataList;

        public List<BaseItemList> SpawnItemLIst => spawnItemList;
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
            BlockItemDatabase blockDB = GameManager.Instance.BlockItemDB;
            GeneratorItemDatabase generatorDB = GameManager.Instance.GeneratorItemDB;
            if (blockDB == null || generatorDB == null) return;

            if (spawnItemList == null) spawnItemList = new List<BaseItemList>();
            spawnItemList.Clear();

            //생성기의 카테고리가 Pet일 때, Pet 블록 아이템, DCO 생성기 블록 드롭가능
            if (ItemGeneratorType == ItemGeneratorType.Pet)
            {
                foreach (GeneratorItemList list in generatorDB.GeneratorItemList)
                {
                    if (list.ItemGeneratorType == ItemGeneratorType.Dco)
                    {
                        if (!spawnItemList.Contains(list)) spawnItemList.Add(list);
                    }
                }
            }
            //생성기의 카테고리가 Dco일 때, 전카테고리 중 랜덤 1렙 생성기 드롭
            else if (ItemGeneratorType == ItemGeneratorType.Dco)
            { 
                
            }

            //생성기 카테고리 == 블록 카테고리인 리스트 분류
            foreach (BlockItemList list in blockDB.BlockItemList)
            {
                if (ItemGeneratorType.ToString() == list.ItemBlockType.ToString())
                {
                    if (!spawnItemList.Contains(list)) spawnItemList.Add(list);
                }
            }
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
