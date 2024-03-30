using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class Stamp : MonoBehaviour
    {
        private StampShadow m_stampShadow;

        private SpriteRenderer m_spriteRenderer;

        [SerializeField] private float m_fadeTime = 1.0f;
        [SerializeField] private Ease m_fadeEase = Ease.Linear;

        private void Awake()
        {
            m_stampShadow = FindObjectOfType<StampShadow>();

            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            m_stampShadow.OnRise += OnRise;
        }

        private void OnDestroy()
        {
            m_stampShadow.OnRise -= OnRise;
        }

        private void OnRise()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => m_spriteRenderer.color.a, SetAlpha, 0.0f, m_fadeTime).SetEase(m_fadeEase));
            sequence.AppendCallback(() => Destroy(gameObject));
        }

        private void SetAlpha(float _alpha)
        {
            var color = m_spriteRenderer.color;
            color.a = _alpha;
            m_spriteRenderer.color = color;
        }
    }
}
