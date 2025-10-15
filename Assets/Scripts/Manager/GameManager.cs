using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DreamNest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private BlockItemDatabase blockItemDB;
        [SerializeField] private GeneratorItemDatabase generatorItemDB;

        public BlockItemDatabase BlockItemDB => blockItemDB;
        public GeneratorItemDatabase GeneratorItemDB => generatorItemDB;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        private void Start()
        {
            blockItemDB.BuildDictionary();

            foreach (var generatorItemList in generatorItemDB.GeneratorItemList)
            {
                generatorItemList.InitializeSpawnList();
            }
        }
    }
}
