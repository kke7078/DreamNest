using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamNest
{
    public class PrefabBase : MonoBehaviour, IItemBaseData, IPointerClickHandler
        /*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
    {
        private float lastClickTime = 0f;
        private const float doubleClickCheckTime = 0.25f;   //0.25초 이내면 더블 클릭

        [SerializeField] private string itemId;
        [SerializeField] private int itemLevel;

        public string ItemID => itemId;
        public int ItemLevel => itemLevel;

        public void OnPointerClick(PointerEventData eventData)
        {
            float clickTime = Time.time - lastClickTime;

            //더블클릭
            if (clickTime <= doubleClickCheckTime)
            {
                CancelInvoke(nameof(OnSingleClick));  //싱글클릭 지연이벤트 취소

                OnDoubleClick();
                lastClickTime = 0f;
            }
            else //싱글 클릭
            {
                lastClickTime = Time.time;
                Invoke(nameof(OnSingleClick), doubleClickCheckTime);
            }
        }

        protected virtual void OnSingleClick()
        {
            Debug.Log("싱글클릭");
        }

        protected virtual void OnDoubleClick() 
        {
            Debug.Log("더블클릭");
        }
    }
}
