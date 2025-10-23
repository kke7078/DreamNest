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
        public override void InitItem(string id)
        {
            BlockItemData data = GameManager.Instance.BlockItemDB.GetItemById(id);

            if (data == null) return;

            //PrefabBase의 필드
            ItemID = id;
            ItemLevel = data.ItemLevel;
        }
    }
}
