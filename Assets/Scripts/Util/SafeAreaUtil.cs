using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public class SafeAreaUtil : MonoBehaviour
    {
        public static void ApplySafeAreaPadding(RectTransform panel)
        { 
            Rect safeArea = Screen.safeArea;
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            //UI 앵커 설정
            panel.anchorMin = new Vector2(0, 0);
            panel.anchorMax = new Vector2(1, 1);
            panel.pivot = new Vector2(0.5f, 0.5f);

            // UI 크기 설정
            float left = safeArea.xMin;
            float right = screenSize.x - safeArea.xMax;
            float bottom = safeArea.yMin;
            float top = screenSize.y - safeArea.yMax;

            // 패딩 적용
            panel.offsetMin = new Vector2(left, bottom);
            panel.offsetMax = new Vector2(-right, -top);
        }
    }
}