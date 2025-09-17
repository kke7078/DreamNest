using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    public class PrefabBase : MonoBehaviour, IItemBaseData
    {
        [SerializeField] private string itemId;

        public string ItemID => itemId;



        public void OnItemClick()
        {
            Debug.Log(ItemID);
        }
    }
}
