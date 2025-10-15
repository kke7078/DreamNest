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

            //�������� ���� ������ �ּ� ���� ���� �������� ���ٸ� false ��ȯ
            if (minSpawnLevel > currentItemLevel) return false;

            //������ ����
            SpawnRandomItem(generator, currentItemLevel);

            return true;
        }

        private void SpawnRandomItem(GeneratorData generatorData, int currentItemLevel)
        {
            //�������� ī�װ��� Pet�� ��, 98:2 ������ DCO ������ ��� ��Ӱ����ؾ� ��
            if (generatorData.GeneratorType == ItemGeneratorType.Pet)
            {
                int rand = Random.Range(0, 100);
                Debug.Log($"rand : {rand}");

                //Dco ������ ��� ����
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

                                    //�ش� ���̵� ���� ������ ����
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