using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DreamNest
{
    public class PrefabBase : MonoBehaviour, IItemBaseData, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] BlockItemDatabase blockItemDatabase;
        [SerializeField] ItemMainType mainType;
        [SerializeField] ItemBlockType blockType;
        [SerializeField] ItemGeneratorType generatorType;
        [SerializeField] private string itemId;

        private float lastClickTime = 0f;
        private const float doubleClickCheckTime = 0.25f;   //0.25�� �̳��� ���� Ŭ��

        public BlockItemDatabase BlockItemDatabase => blockItemDatabase;

        public string ItemID => itemId;

        public void OnPointerClick(PointerEventData eventData)
        {
            float clickTime = Time.time - lastClickTime;

            //����Ŭ��
            if (clickTime <= doubleClickCheckTime)
            {
                Debug.Log("����Ŭ��");
                OnDoubleClick();
                lastClickTime = 0f;
            }
            else //�̱� Ŭ��
            {
                lastClickTime = Time.time;
                Invoke("OnSingleClick", doubleClickCheckTime);
            }
        }

        protected virtual void OnDoubleClick() { }
        protected virtual void OnSingleClick() { }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PrefabBase), true)]
    public class PrefabBaseEditor : Editor
    {
        SerializedProperty itemMainTypeProp;
        SerializedProperty itemBlockTypeProp;
        SerializedProperty itemGeneratorTypeProp;

        void OnEnable()
        {
            itemMainTypeProp = serializedObject.FindProperty("mainType");
            itemBlockTypeProp = serializedObject.FindProperty("blockType");
            itemGeneratorTypeProp = serializedObject.FindProperty("generatorType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(itemMainTypeProp);

            ItemMainType mainType = (ItemMainType)itemMainTypeProp.enumValueIndex;
            switch (mainType)
            {
                case ItemMainType.Block:

                    EditorGUILayout.PropertyField(itemBlockTypeProp);
                    break;
                case ItemMainType.Generator:
                    EditorGUILayout.PropertyField(itemGeneratorTypeProp);
                    break;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("generatorItemDatabase"));
            DrawInspectorBase();
            serializedObject.ApplyModifiedProperties();
        }

        public virtual void DrawInspectorBase() {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("blockItemDatabase"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("itemId"));
        }
    }
#endif
}
