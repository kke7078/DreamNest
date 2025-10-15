using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DreamNest
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance { get; private set; }

        [SerializeField] private SpawnRateTable spawnRateNormal;
        [SerializeField] private SpawnRateTable spawnRateRare;
        [SerializeField] private PrefabGenerator prefabGenerator;
        //[SerializeField] private PrefabBlock prefabBlock;

        public SpawnRateTable SpawnRateNormal => spawnRateNormal;
        public SpawnRateTable SpawnRateRare => spawnRateRare;
        public PrefabGenerator PrefabGenerator => prefabGenerator;

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

        public bool TrySpawnItem(GeneratorData generator, int currentItemLevel)
        {
            int minSpawnLevel = 0;

            switch (generator.ItemGrade)
            {
                case ItemGrade.Normal: minSpawnLevel = spawnRateNormal.MinSpawnLevel; break;
                case ItemGrade.Rare: minSpawnLevel = spawnRateRare.MinSpawnLevel; break;
            }

            //생성기의 현재 레벨이 최소 생성 가능 레벨보다 낮다면 false 반환
            if (minSpawnLevel > currentItemLevel) return false;

            //아이템 생성
            SpawnRandomItem(generator, currentItemLevel);

            return true;
        }

        private void SpawnRandomItem(GeneratorData generatorData, int currentItemLevel)
        {
            //생성기의 카테고리가 Pet일 시, 98:2 비율로 DCO 생성기 블록 드롭가능해야 함
            if (generatorData.GeneratorType == ItemGeneratorType.Pet)
            {
                int rand = Random.Range(0, 100);
                Debug.Log($"rand : {rand}");

                //Dco 생성기 블록 스폰
                if (rand > 98)
                {
                    foreach (BaseItemList list in generatorData.Generator.SpawnItemLIst)
                    {
                        if (list is GeneratorItemList) SpawnItem(list, currentItemLevel);
                    }
                }
                else 
                {
                    foreach (BaseItemList list in generatorData.Generator.SpawnItemLIst)
                        if(list is BlockItemList) SpawnItem(list, currentItemLevel);
                }
            }
        }

        private void SpawnItem(BaseItemList list, int currentItemLevel)
        {
            bool isBlock = list is BlockItemList;
            float rand = Random.Range(0, 1f);
            float currentWeight = 0;

            switch (list.ItemGrade)
            {
                case ItemGrade.Normal:
                    if (normalRate.TryGetValue(currentItemLevel, out List<float> rates))
                    {
                        for (int i = 0; i < rates.Count; i++)
                        {
                            currentWeight += rates[i];

                            if (rand <= currentWeight)
                            {
                                if (isBlock)
                                {
                                    BlockItemList blockList = list as BlockItemList;
                                    string id = blockList.ItemDataList[i].ItemID;
                                    PrefabGenerator newItem = Instantiate(PrefabGenerator);
                                    newItem.SetItemInfo(id, 1);

                                    //해당 아이디를 가진 아이템 생성
                                    BaseItemData item = GameManager.Instance.BlockItemDB.GetItemById(id);
                                    
                                }
                                
                                return;
                            }
                        }
                    }
                    break;
                case ItemGrade.Rare:
                    break;
            }
        }
    }
}