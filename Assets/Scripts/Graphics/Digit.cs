using UnityEngine;

namespace IP1
{
    public class Digit : MonoBehaviour
    {
        private SpriteAnimationSet m_animationSet;

        private int m_currentDigit;

        public int CurrentDigit
        {
            get => m_currentDigit;
            set
            {
                if(value == m_animationSet.CurrentAnimationIndex) { return; }
                if (value > 9) { value = 0; }
                m_animationSet.CurrentAnimationIndex = value;
            }
        }
        
        private void Awake()
        {
            m_animationSet = GetComponentInChildren<SpriteAnimationSet>();
        }
    }
}
