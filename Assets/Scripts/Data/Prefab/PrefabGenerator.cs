using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace DreamNest
{
    public class PrefabGenerator : PrefabBase
    {
        private int currentGeneratorCount;
        private bool isOnCooldown;
        private SpawnManager spawnManager;

        [SerializeField] List<BaseItemList> spawnList;
        [SerializeField] ItemGeneratorType generatorType;
        [SerializeField] ItemGrade itemGrade;
        [SerializeField] float itemGeneratorCount;
        [SerializeField] float itemCooltimeDuration;

        public List<BaseItemList> SpawnList => spawnList;
        public ItemGeneratorType GeneratorType => generatorType;
        public ItemGrade ItemGrade => itemGrade;
        public float ItemGeneratorCount => itemGeneratorCount;
        public float ItemCooltimeDuration => itemCooltimeDuration;


        private void Awake()
        {
            spawnManager = SpawnManager.Instance;
        }

        public void Start()
        {
            InitItem("Pet006"); //테스트용
        }

        protected override void InitItem(string id)
        {
            GeneratorItemInfo data = GameManager.Instance.GeneratorItemDB.GetItemById(id);

            if (data != null) 
            {
                //PrefabBase의 필드
                ItemLevel = data.Data.ItemLevel;

                //PrefabGenerator의 필드
                generatorType = data.List.ItemGeneratorType;
                itemGrade = data.List.ItemGrade;
                itemGeneratorCount = data.Data.MaxGenerationCount;

                if (spawnList.Count == 0)
                {
                    foreach (var list in GameManager.Instance.BlockItemDB.BlockItemList)
                    {
                        if (list.ItemBlockType.ToString() == generatorType.ToString())
                        {
                            if (!spawnList.Contains(list)) spawnList.Add(list);
                        }
                    }

                    //예외 카테고리 세팅
                    switch(generatorType)
                    {
                        case ItemGeneratorType.Pet:
                            foreach (var list in GameManager.Instance.GeneratorItemDB.GeneratorItemList)
                            {
                                if(list.ItemGeneratorType == ItemGeneratorType.Dco)
                                {
                                    if (!spawnList.Contains(list)) spawnList.Add(list);
                                }
                            }

                            break;
                    }
                }
            }
        }

        //더블 클릭
        protected override void OnDoubleClick()
        {
            if (isOnCooldown) return;   //쿨타임 중이면 실행X

            //생성기의 최대생성 수보다 현재 생성 수가 더 작을 때
            if (ItemGeneratorCount > currentGeneratorCount)
            {
                //아이템 규칙 통과 확인
                if (spawnManager.TrySpawnItem(this, ItemLevel))
                {
                    currentGeneratorCount++;

                    //아이템 최대 생성 수만큼 생성되었을 때
                    if (ItemGeneratorCount == currentGeneratorCount)
                    {
                        switch (ItemGrade)
                        {
                            case ItemGrade.Normal:
                                StartCoroutine(StartCooldown());
                                break;
                            case ItemGrade.Rare:
                                Destroy(gameObject);
                                break;
                        }
                    }
                }
            }
        }

        //쿨타임 시작
        private IEnumerator StartCooldown()
        {
            isOnCooldown = true;

            Debug.Log("쿨타임 실행");

            yield return new WaitForSeconds(ItemCooltimeDuration);

            Debug.Log("쿨타임 종료");

            currentGeneratorCount = 0;
            isOnCooldown = false;
        }
    }
}
