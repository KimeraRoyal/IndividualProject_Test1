using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    [Serializable]
    public class IndividualSpriteAnimation
    {
        [SerializeField] private Sprite[] m_frames;

        [SerializeField] private float m_frameTime = 1.0f;
        [SerializeField] private float m_frameTimeVariance = 0.0f;

        [SerializeField] private bool m_startOnRandomFrame;
        [SerializeField] private bool m_randomize;

        public IReadOnlyList<Sprite> Frames => m_frames;

        public float FrameTime => m_frameTime;
        public float FrameTimeVariance => m_frameTimeVariance;

        public bool Randomize => m_randomize;
        public bool StartOnRandomFrame => m_startOnRandomFrame;
    }
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationSet : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;
        
        [SerializeField] private IndividualSpriteAnimation[] m_animations;
        
        private int m_currentAnimation;
        private int m_currentFrame;
        private float m_timer;
        private float m_uniqueFrameTime;

        public int CurrentAnimationIndex
        {
            get => m_currentAnimation;
            set
            {
                if(m_animations.Length < 1) { return; }
                
                value = Math.Clamp(value, 0, m_animations.Length);
                m_currentAnimation = value;
                OnAnimationChange?.Invoke(m_currentAnimation);
            }
        }

        public IndividualSpriteAnimation CurrentAnimation => m_animations[m_currentAnimation];

        public int CurrentFrame
        {
            get => m_currentFrame;
            set
            {
                if(m_animations.Length < 1 || CurrentAnimation.Frames.Count < 1) { return; }
                
                value = Math.Clamp(value, 0, CurrentAnimation.Frames.Count);
                m_currentFrame = value;
                OnFrameChange?.Invoke(m_currentFrame);
            }
        }

        public Action<int> OnAnimationChange;
        public Action<int> OnFrameChange;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            OnAnimationChange += _animation => { StartAnimation(m_animations[m_currentAnimation]); };
            OnFrameChange += _frame => { m_spriteRenderer.sprite = CurrentAnimation.Frames[_frame]; };

            CurrentAnimationIndex = 0;
        }

        private void Update()
        {
            m_timer += Time.deltaTime;
            if(m_timer < m_uniqueFrameTime) { return; }
            m_timer -= m_uniqueFrameTime;

            var frameCount = CurrentAnimation.Frames.Count;
            
            if(m_animations.Length < 1 || CurrentAnimation.Frames.Count < 1) { return; }

            if (CurrentAnimation.Randomize) { CurrentFrame = Random.Range(0, CurrentAnimation.Frames.Count); }
            else { CurrentFrame = (m_currentFrame + 1) % frameCount; }
        }

        private void StartAnimation(IndividualSpriteAnimation _animation)
        {
            m_uniqueFrameTime = Random.Range(_animation.FrameTime - _animation.FrameTimeVariance, _animation.FrameTime + _animation.FrameTimeVariance);
            CurrentFrame = _animation.StartOnRandomFrame ? Random.Range(0, _animation.Frames.Count) : 0;
        }
    }
}
