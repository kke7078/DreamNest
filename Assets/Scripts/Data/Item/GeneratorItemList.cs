using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    [CreateAssetMenu(menuName = "Data/GeneratorItemList")]
    public class GeneratorItemList : BaseItemList
    {
        [SerializeField] private List<GeneratorItemData> itemDataList;
        public List<GeneratorItemData> ItemDataList => itemDataList;

        //����Ʈ ������ ���� �Է�
        public void OnEnable()
        {
            if (itemDataList == null) itemDataList = new List<GeneratorItemData>();
            SetItemInfo(itemDataList);
        }
    }
}
