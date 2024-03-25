using System;
using UnityEngine;

namespace IP1
{
    public class FaceButton : MonoBehaviour
    {
        private bool m_pressed;

        public bool Pressed
        {
            get => m_pressed;
            set
            {
                if(m_pressed == value) { return; }

                m_pressed = value;
                OnPressedChange?.Invoke(m_pressed);
            }
        }

        public Action<bool> OnPressedChange;
    }
}
