using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace IP1
{
    [RequireComponent(typeof(Image))]
    public class FadeImageInOut : MonoBehaviour
    {
        private Image m_image;

        [SerializeField] private bool m_fadeInOnStart;
        
        [SerializeField] private float m_fadeInTime = 1.0f;
        [SerializeField] private Ease m_fadeInEase = Ease.Linear;
        
        [SerializeField] private float m_fadeOutTime = 1.0f;
        [SerializeField] private Ease m_fadeOutEase = Ease.Linear;

        private Tween m_tween;

        private void Awake()
        {
            m_image = GetComponent<Image>();
        }

        private void Start()
        {
            if(!m_fadeInOnStart) { return; }
            Fade(false);
        }

        public void Fade(bool _visible)
        {
            if(m_tween is { active: true }) { m_tween.Kill(); }
            
            var time = _visible ? m_fadeOutTime : m_fadeInTime;
            var ease = _visible ? m_fadeOutEase : m_fadeInEase;
            
            var targetColor = m_image.color;
            targetColor.a = _visible ? 1.0f : 0.0f;

            if (time < 0.001f)
            {
                m_image.color = targetColor;
            }
            else
            {
                m_tween = DOTween.To(() => m_image.color, _color => m_image.color = _color, targetColor, time).SetEase(ease);
            }
        }
    }
}
