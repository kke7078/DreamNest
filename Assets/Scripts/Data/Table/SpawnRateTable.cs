using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class SpawnRate
    {
        [SerializeField] private int level;
        [SerializeField] private List<float> weight;

        public int Level => level;
        public List<float> Weight => weight;
    }

    [CreateAssetMenu(menuName = "Table/SpawnRateTable")]
    public class SpawnRateTable : ScriptableObject
    {
        [SerializeField] private ItemGrade itemGrade;
        [SerializeField] private int minSpawnLevel;
        [SerializeField] private List<SpawnRate> spawnRate;

        public ItemGrade ItemGrade => itemGrade;
        public int MinSpawnLevel => minSpawnLevel;
        public List<SpawnRate> SpawnRate => spawnRate;
    }
}
