using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace mp
{
    public class SlotManager : MonoBehaviour    //머지판 위에 어떤 아이템이 어디에 놓여있는 지 관리하는 클래스
    {
        [SerializeField] private RectTransform slotParent;
        [SerializeField] private GameObject slotPrefab;

        private RectTransform rectTransform;
        private GridLayoutGroup gridLayout;
        
        private int slotColCount = 7;
        private int slotRowCount = 10;
        private float slotSpacing = 0f;
        private float slotPadding = 0f;

        private Vector2 lastSize;   //실제로 화면의 크기가 변경되었는지 확인하기 위한 변수

        private int SlotTotalCount => slotColCount * slotRowCount;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            gridLayout = slotParent.GetComponent<GridLayoutGroup>();

            slotSpacing = gridLayout.spacing.x;
            slotPadding = gridLayout.padding.left + gridLayout.padding.right;

            lastSize = rectTransform.rect.size;  //초기 크기를 저장

            CreateSlot();
            UpdateCellSize();
        }

        private void OnRectTransformDimensionsChange()  //rectTransform 의 크기가 변경되었을 때 호출됨
        {
            // * safeArea 초기화 시 Awake보다 일찍 호출될 수 있으므로, null 체크 필요! *
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            if(gridLayout == null) gridLayout = slotParent.GetComponent<GridLayoutGroup>();

            Vector2 currentSize = rectTransform.rect.size;

            if (currentSize != lastSize)
            {
                lastSize = currentSize;  //현재 크기를 저장
                UpdateCellSize();
            }
        }

        private void CreateSlot()
        {
            if (slotParent.childCount > 0)
            {
                foreach (Transform child in slotParent)
                {
                    Destroy(child.gameObject);  // 모든 자식 오브젝트 제거
                }
            }

            for (int i = 0; i < SlotTotalCount; i++)
            {
                GameObject slot = Instantiate(slotPrefab, slotParent);  // 슬롯 생성
            }
        }

        private void UpdateCellSize()
        {
            float boardWidth = Mathf.Min(rectTransform.rect.width, 1100f);  //보드 최대 너비값 설정
            
            float slotSize = Mathf.Floor( ( boardWidth - slotPadding - ( slotSpacing * (slotColCount - 1) ) ) / slotColCount );   //슬롯 크기 계산
            float slotTotalHeight = (slotSize * slotRowCount) + (slotSpacing * (slotRowCount - 1)) + slotPadding; // 슬롯 총 높이 계산

            //슬롯 크기 설정
            if (slotTotalHeight > rectTransform.rect.height)    //만약 슬롯의 총 높이값이 보드판의 높이보다 크다면 슬롯 크기 재계산
            {
                slotSize = Mathf.Floor((rectTransform.rect.height - slotPadding  - (slotSpacing * (slotRowCount - 1))) / slotRowCount);
            }
            gridLayout.cellSize = new Vector2(slotSize, slotSize);
        }
    }
}
