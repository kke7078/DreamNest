using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class GeneratorData
    {
        [SerializeField] GeneratorItemList generator;
        [SerializeField] ItemGeneratorType generatorType;
        [SerializeField] ItemGrade itemGrade;
        [SerializeField] int itemGeneratorCount;
        [SerializeField] float itemCooltimeDuration;
        //[SerializeField] ItemPrice itemBuyPrice;

        public GeneratorItemList Generator => generator;
        public ItemGeneratorType GeneratorType => generatorType;
        public ItemGrade ItemGrade => itemGrade;
        public int ItemGeneratorCount => itemGeneratorCount;
        public float ItemCooltimeDuration => itemCooltimeDuration;
        //public ItemPrice ItemBuyPrice => itemBuyPrice;
    }


    public class PrefabGenerator : PrefabBase
    {
        [SerializeField] SpawnManager spawnManager;
        [SerializeField] private GeneratorData generatorData;

        private int currentGeneratorCount;
        private bool isOnCooldown;

        private void Awake()
        {
            spawnManager = SpawnManager.Instance;
        }

        protected override void OnDoubleClick()
        {
            if (isOnCooldown) return;   //쿨타임 중이면 실행X

            //생성기의 최대생성 수보다 현재 생성 수가 더 작을 때
            if (generatorData.ItemGeneratorCount > currentGeneratorCount)
            {
                //아이템 규칙 통과 확인
                if (spawnManager.TrySpawnItem(generatorData, ItemLevel))
                {
                    currentGeneratorCount++;

                    if (generatorData.ItemGeneratorCount == currentGeneratorCount)
                    {
                        switch (generatorData.ItemGrade)
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

        private IEnumerator StartCooldown()
        {
            isOnCooldown = true;

            Debug.Log("쿨타임 실행");

            yield return new WaitForSeconds(generatorData.ItemCooltimeDuration);

            Debug.Log("쿨타임 종료");

            currentGeneratorCount = 0;
            isOnCooldown = false;
        }
    }
}
