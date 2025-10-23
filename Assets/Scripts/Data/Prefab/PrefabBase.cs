using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamNest
{
    public abstract class PrefabBase : MonoBehaviour, IItemBaseData, IPointerClickHandler
        /*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
    {
        private float lastClickTime = 0f;
        private const float doubleClickCheckTime = 0.25f;   //0.25초 이내면 더블 클릭

        public bool isReword = false; //test

        [field: SerializeField] public string ItemID { get; protected set; }
        [field: SerializeField] public int ItemLevel { get; protected set; }

        private void Start()
        {
            if (GetComponentInParent<RewordArea>() != null) isReword = true;
            else isReword = false;
        }

        //아이템 정보 세팅
        public abstract void InitItem(string id);

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isReword) 
            {
                SpawnManager.Instance.CreateItem(ItemID);   //생성
                SlotManager.Instance.RewordArea.RemoveRewordItem();

                return;
            }


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

        protected virtual void OnSingleClick() {}

        protected virtual void OnDoubleClick() {}
    }
}
