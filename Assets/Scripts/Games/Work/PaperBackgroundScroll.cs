using DG.Tweening;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(ScrollSprite))]
    public class PaperBackgroundScroll : MonoBehaviour
    {
        private StampArm m_stampArm;

        private ScrollSprite m_scrollSprite;

        [SerializeField] private Vector2 m_stampSpeedIncrease;
        [SerializeField] private float m_speedIncreaseDuration = 1.0f;

        private Tween m_tween;

        private void Awake()
        {
            m_stampArm = FindObjectOfType<StampArm>();

            m_scrollSprite = GetComponent<ScrollSprite>();
        }

        private void Start()
        {
            m_stampArm.OnPaperStamped += OnPaperStamped;
        }

        private void OnPaperStamped()
        {
            if(m_tween is { active: true }) { m_tween.Kill(); }

            m_tween = DOTween.To(() => m_scrollSprite.Speed, _speed => m_scrollSprite.Speed = _speed, m_scrollSprite.Speed + m_stampSpeedIncrease, m_speedIncreaseDuration);
        }
    }
}
