using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

namespace DreamNest
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance { get; private set; }

        [SerializeField] private SpawnRateTable spawnRateNormal;
        [SerializeField] private SpawnRateTable spawnRateRare;
        [SerializeField] private PrefabGenerator prefabGenerator;
        [SerializeField] private PrefabBlock prefabBlock;

        public SpawnRateTable SpawnRateNormal => spawnRateNormal;
        public SpawnRateTable SpawnRateRare => spawnRateRare;
        public PrefabGenerator PrefabGenerator => prefabGenerator;
        public PrefabBlock PrefabBlock => prefabBlock;

        Dictionary<int, List<float>> normalRate = new Dictionary<int, List<float>>();
        Dictionary<int, List<float>> rareRate = new Dictionary<int, List<float>>();

        private void Awake()
        {
            Instance = this;

            LoadSpawnRate();
        }

        private void LoadSpawnRate()
        {
            if (normalRate.Count > 0 && rareRate.Count > 0) return;

            foreach (var entry in SpawnRateNormal.SpawnRate) normalRate.Add(entry.Level, entry.Weight);
            foreach (var entry in SpawnRateRare.SpawnRate) rareRate.Add(entry.Level, entry.Weight);
        }

        //생성 가능 조건 필터링
        public bool TrySpawnItem(PrefabGenerator generator, int currentItemLevel)
        {
            int minSpawnLevel = 0;

            switch (generator.ItemGrade)
            {
                case ItemGrade.Normal: minSpawnLevel = spawnRateNormal.MinSpawnLevel; break;
                case ItemGrade.Rare: minSpawnLevel = spawnRateRare.MinSpawnLevel; break;
            }

            //생성기의 현재 레벨이 최소 생성 가능 레벨보다 낮다면 false 반환
            if (minSpawnLevel > currentItemLevel) return false;

            //아이템 리스트 뽑기
            SpawnRandomItem(generator, currentItemLevel);

            return true;
        }
        
        private void SpawnRandomItem(PrefabGenerator generator, int currentItemLevel)
        {
            if (generator != null)
            {
                float rand = Random.Range(0f, 1f);
                float currentWeight = 0f;

                switch (generator.ItemGrade)
                {
                    case ItemGrade.Normal:  //Pet, Dco
                        if (normalRate.TryGetValue(currentItemLevel, out List<float> normalRates))
                        {
                            if (generator.GeneratorType == ItemGeneratorType.Dco)
                            {
                                //spawnList에 들어있는 생성기 리스트들 중 하나를 골라서 아이템을 뽑는다
                                GeneratorItemList gil = generator.SpawnList[Random.Range(0, generator.SpawnList.Count)] as GeneratorItemList;

                                for (int i = 0; i < normalRates.Count; i++)
                                {
                                    currentWeight += normalRates[i];

                                    if (rand < currentWeight)
                                    {
                                        GeneratorItemData gid = gil.ItemDataList[i];

                                        CreateItem(gid.ItemID);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                int petRand = Random.Range(0, 100);

                                if (petRand < 98) //pet blockItem 호출
                                {
                                    for (int i = 0; i < normalRates.Count; i++)
                                    {
                                        currentWeight += normalRates[i];

                                        if (rand < currentWeight)
                                        {
                                            //i 레벨 템 호출
                                            BlockItemList bil = generator.SpawnList.OfType<BlockItemList>().FirstOrDefault();
                                            BlockItemData bid = bil.ItemDataList[i];

                                            CreateItem(bid.ItemID);
                                            return;
                                        }
                                    }
                                }
                                else //dco generatorItem 호출 :: 1렙짜리 생성기 아이템을 뽑는다
                                { 
                                    GeneratorItemList gil = generator.SpawnList.OfType<GeneratorItemList>().FirstOrDefault();
                                    GeneratorItemData gid = gil.ItemDataList[0];

                                    CreateItem(gid.ItemID);
                                    return;
                                }
                            }
                        }
                        break;
                    case ItemGrade.Rare:    // 그 외
                        if (rareRate.TryGetValue(currentItemLevel, out List<float> rareRates))
                        {
                            for (int i = 0; i < rareRates.Count; i++)
                            {
                                currentWeight += rareRates[i];

                                if (rand < currentWeight)
                                {
                                    //i 레벨 템 호출
                                    BlockItemList bil = generator.SpawnList.OfType<BlockItemList>().FirstOrDefault();
                                    BlockItemData bid = bil.ItemDataList[i];

                                    CreateItem(bid.ItemID);
                                    return;
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void CreateItem(string id)
        {
            PrefabBase prefObj = null;

            if (GameManager.Instance.GeneratorItemDB.GetItemById(id) == null)
            {
                prefObj = Instantiate(PrefabBlock);
            }
            else
            {
                prefObj = Instantiate(PrefabGenerator);
            }

            SlotManager.Instance.SetSlotItem(prefObj);
        }
    }
}