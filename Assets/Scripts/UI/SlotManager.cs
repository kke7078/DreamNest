using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public class SlotManager : MonoBehaviour    //머지판 위에 어떤 아이템이 어디에 놓여있는 지 관리
    {
        [SerializeField] private GameObject slotPrefab;
        private RectTransform rectTransform;

        private int slotColCount = 7;
        private int slotRowCount = 10;
        private int slotSpacing = 5;
        private int SlotTotalCount => slotColCount * slotRowCount;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            StartCoroutine("SlotSetting");
        }

        IEnumerator SlotSetting()
        {
            yield return null;

            // slot 크기 설정
            int slotWidth = ((int)rectTransform.rect.width - (slotSpacing * (slotColCount - 1)) - 20) / slotColCount;
            int slotHeight = ((int)rectTransform.rect.height - (slotSpacing * (slotRowCount - 1)) - 20) / slotRowCount;

            //slot 복제
            for (int i = 0; i < SlotTotalCount + 7; i++) 
            { 
                GameObject slot = Instantiate(slotPrefab, rectTransform);
            };
        }
    }
}
