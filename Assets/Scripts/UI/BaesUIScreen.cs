using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    public class BaesUIScreen : MonoBehaviour
    {
        private RectTransform panel;
        private Rect lastSafeArea;

        private void Awake()
        {
            panel = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void Update()
        {
            // 화면 크기나 안전 영역이 변경되었는지 확인
            if (lastSafeArea != Screen.safeArea)
            {
                ApplySafeArea();
            }
        }

        private void ApplySafeArea()
        {
            SafeAreaUtil.ApplySafeAreaPadding(panel);
            lastSafeArea = Screen.safeArea;
        }    
    }
}
