using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mp
{
    public class BaesUIScreen : MonoBehaviour
    {
        private RectTransform panel;

        private void Awake()
        {
            panel = GetComponent<RectTransform>();
            SafeAreaUtil.ApplySafeAreaPadding(panel);
        }
    }
}
