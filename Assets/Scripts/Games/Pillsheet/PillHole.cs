using System;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(FaceButton), typeof(SpriteAnimationSet))]
    public class PillHole : MonoBehaviour
    {
        private FaceButton m_button;
        private SpriteAnimationSet m_animationSet;

        private bool m_open;

        public bool Open
        {
            get => m_open;
            set
            {
                if(m_open == value) { return; }
                m_open = value;
                m_animationSet.CurrentAnimationIndex = m_open ? 1 : 0;
            }
        }

        public Action OnPopped;

        private void Awake()
        {
            m_button = GetComponent<FaceButton>();
            m_animationSet = GetComponent<SpriteAnimationSet>();
        }

        private void Start()
        {
            m_button.OnPressedChange += OnPressedChange;
        }

        private void OnPressedChange(bool _pressed)
        {
            if(Open) { return; }
            Open = _pressed;
            if(Open) { OnPopped?.Invoke(); }
        }
    }
}
