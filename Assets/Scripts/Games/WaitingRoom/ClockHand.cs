using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class ClockHand : MonoBehaviour
    {
        private Clock m_clock;

        [SerializeField] private int m_interval = 1;

        [SerializeField] private Vector3 m_handMovementAngle = Vector3.forward;

        [SerializeField] private float m_handMovementDuration = 0.1f;
        [SerializeField] private Ease m_handMovementEase = Ease.Linear;

        private Tween m_handMovementTween;
        
        private void Awake()
        {
            m_clock = GetComponentInParent<Clock>();
        }

        private void Start()
        {
            m_clock.OnTick += OnTick;

            transform.eulerAngles = m_handMovementAngle * CalculateAngle(m_clock.CurrentTick);
        }

        private void OnTick(int _ticks)
        {
            if(_ticks % m_interval != 0) { return; }

            if (m_handMovementTween is { active: true }) { m_handMovementTween.Kill(); }

            m_handMovementTween = transform.DORotate(m_handMovementAngle * CalculateAngle(_ticks), m_handMovementDuration).SetEase(m_handMovementEase);
        }

        private int CalculateAngle(int _ticks)
            => ((_ticks / m_interval) % 60) * 6;
    }
}
