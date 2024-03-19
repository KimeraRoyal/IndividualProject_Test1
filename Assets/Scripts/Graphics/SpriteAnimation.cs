using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;
        
        [SerializeField] private Sprite[] m_frames;

        [SerializeField] private float m_frameTime = 1.0f;
        [SerializeField] private float m_frameTimeVariance = 0.0f;

        private int m_currentFrame;
        private float m_timer;
        private float m_uniqueFrameTime;

        public Action<int> OnFrameChange;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            m_uniqueFrameTime = Random.Range(m_frameTime - m_frameTimeVariance, m_frameTime + m_frameTimeVariance);
        }

        private void Start()
        {
            OnFrameChange += _frame => { m_spriteRenderer.sprite = m_frames[_frame]; };
        }

        private void Update()
        {
            m_timer += Time.deltaTime;
            if(m_timer < m_uniqueFrameTime) { return; }
            m_timer -= m_uniqueFrameTime;

            m_currentFrame = (m_currentFrame + 1) % m_frames.Length;
            OnFrameChange?.Invoke(m_currentFrame);
        }
    }
}
