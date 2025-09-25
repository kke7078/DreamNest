using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    public interface IItemBaseData
    { 
        public string ItemID { get; }
        public int ItemLevel { get; }
        //public string ItemName { get; }
        //public string ItemDesc { get; }
    }
}
