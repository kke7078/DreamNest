using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class GeneratorData
    {
        [SerializeField] ItemGeneratorType generatorType;
        [SerializeField] ItemGrade itemGrade;
        [SerializeField] int itemGeneratorCount;
        [SerializeField] float itemCooltimeDuration;
        //[SerializeField] ItemPrice itemBuyPrice;

        public ItemGeneratorType GeneratorType => generatorType;
        public ItemGrade ItemGrade => itemGrade;
        public int ItemGeneratorCount => itemGeneratorCount;
        public float ItemCooltimeDuration => itemCooltimeDuration;
        //public ItemPrice ItemBuyPrice => itemBuyPrice;
    }


    public class PrefabGenerator : PrefabBase
    {
        [SerializeField] private BlockItemDatabase blockItemDatabase;
        [SerializeField] private GeneratorData generatorData;

        private int currentGeneratorCount;
        private bool isOnCooldown;


        protected override void OnDoubleClick()
        {
            Debug.Log("generator doubleClick");

            if (isOnCooldown)
            {
                return;   //쿨타임 중이면 실행X
            }

            switch (generatorData.ItemGrade)
            {
                case ItemGrade.Normal:
                    if (ItemLevel >= 6)
                    {
                        //생성기의 최대생성 수보다 현재 생성 수가 더 작으면
                        if (generatorData.ItemGeneratorCount > currentGeneratorCount)
                        {
                            blockItemDatabase.GetRandomItem(generatorData); //랜덤 아이템 생성
                            currentGeneratorCount++;

                            Debug.Log($"{currentGeneratorCount}회 생성");
                            //생성기의 최대생성 수와 현재 생성 수가 같아지면 쿨타임 실행
                            if (generatorData.ItemGeneratorCount == currentGeneratorCount)  
                            {
                                StartCoroutine(StartCooldown());
                            }
                        }
                    }
                    break;
                case ItemGrade.Rare:
                    if (ItemLevel >= 3)
                    {
                        //생성기의 최대생성 수보다 현재 생성 수가 더 작으면
                        if (generatorData.ItemGeneratorCount > currentGeneratorCount)
                        {
                            blockItemDatabase.GetRandomItem(generatorData); //랜덤 아이템 생성
                            currentGeneratorCount++;

                            //생성기의 최대생성 수와 현재 생성 수가 같아지면 오브젝트 파괴
                            if (generatorData.ItemGeneratorCount == currentGeneratorCount)
                            {
                                Destroy(gameObject);
                            }
                        }
                    }
                    break;
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
