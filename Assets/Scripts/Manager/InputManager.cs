using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamNest
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        //��������Ʈ �Լ� 
        public event Action OnMouseClick;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            { 
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                OnMouseClick?.Invoke();
            }
        }
    }
}
