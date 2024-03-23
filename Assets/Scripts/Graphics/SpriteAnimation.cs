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

        [SerializeField] private bool m_startOnRandomFrame;
        [SerializeField] private bool m_randomize;

        private int m_currentFrame;
        private float m_timer;
        private float m_uniqueFrameTime;

        public int CurrentFrame
        {
            get => m_currentFrame;
            set
            {
                m_currentFrame = value;
                OnFrameChange?.Invoke(m_currentFrame);
            }
        }

        public Action<int> OnFrameChange;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            m_uniqueFrameTime = Random.Range(m_frameTime - m_frameTimeVariance, m_frameTime + m_frameTimeVariance);
        }

        private void Start()
        {
            OnFrameChange += _frame => { m_spriteRenderer.sprite = m_frames[_frame]; };

            if (m_startOnRandomFrame) { RandomizeFrame(); }
        }

        private void Update()
        {
            m_timer += Time.deltaTime;
            if(m_timer < m_uniqueFrameTime) { return; }
            m_timer -= m_uniqueFrameTime;

            if(m_randomize) { RandomizeFrame(); }
            else { CurrentFrame = (m_currentFrame + 1) % m_frames.Length; }
        }
        
        private void RandomizeFrame()
            => CurrentFrame = Random.Range(0, m_frames.Length);
    }
}
