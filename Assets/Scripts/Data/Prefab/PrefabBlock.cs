using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [System.Serializable]
    public class BlockData
    {
        [SerializeField] ItemBlockType blockType;
        public ItemBlockType BlockType => blockType;
    }

    public class PrefabBlock : PrefabBase
    {
        protected override void InitItem(string id)
        {

        }
    }
}
