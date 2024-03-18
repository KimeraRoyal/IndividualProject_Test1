using System;
using UnityEngine;

namespace IP1
{
    public class SpriteAnimation : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;
        
        [SerializeField] private Sprite[] m_frames;

        [SerializeField] private float m_frameTime = 1.0f;

        private int m_currentFrame;
        private float m_timer;

        public Action<int> OnFrameChange;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            OnFrameChange += _frame => { m_spriteRenderer.sprite = m_frames[_frame]; };
        }

        private void Update()
        {
            m_timer += Time.deltaTime;
            if(m_timer < m_frameTime) { return; }
            m_timer -= m_frameTime;

            m_currentFrame = (m_currentFrame + 1) % m_frames.Length;
            OnFrameChange?.Invoke(m_currentFrame);
        }
    }
}
