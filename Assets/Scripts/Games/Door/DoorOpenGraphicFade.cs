using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace IP1
{
    [RequireComponent(typeof(Image))]
    public class DoorOpenGraphicFade : MonoBehaviour
    {
        private Handle m_handle;

        private Image m_graphic;
        
        [SerializeField] private Gradient m_gradient;
        
        [SerializeField] private float m_fadeDuration = 1.0f;
        [SerializeField] private Ease m_fadeEasing = Ease.Linear;

        private float m_gradientProgression;

        private float GradientProgression
        {
            get => m_gradientProgression;
            set
            {
                m_gradientProgression = value;
                m_graphic.color = m_gradient.Evaluate(m_gradientProgression);
            }
        }

        private void Awake()
        {
            m_handle = FindObjectOfType<Handle>();

            m_graphic = GetComponent<Image>();
        }

        private void Start()
        {
            m_handle.OnOpened += OnOpened;
        }

        private void OnOpened()
        {
            DOTween.To(() => GradientProgression, _value => GradientProgression = _value, 1, m_fadeDuration).SetEase(m_fadeEasing);
        }
    }
}
