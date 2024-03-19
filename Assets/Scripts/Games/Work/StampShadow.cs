using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class StampShadow : MonoBehaviour
    {
        private PaperStack m_paperStack;
        
        [SerializeField] private LayerMask m_boxcastLayerMask;
        [SerializeField] private Vector2 m_boxcastOrigin;
        [SerializeField] private Vector2 m_boxcastSize = Vector2.one;

        [SerializeField] private float m_shadowMoveDuration = 1.0f;
        [SerializeField] private Ease m_shadowMoveEase = Ease.Linear;

        private Tween m_tween;

        private void Awake()
        {
            m_paperStack = FindObjectOfType<PaperStack>();
        }
        
        private void Update()
        {
            var rayHit = Physics2D.BoxCast(m_boxcastOrigin, m_boxcastSize, 0, Vector2.zero, 0, m_boxcastLayerMask);
            if(rayHit.collider == null) { return; }
            
            Move();

            rayHit.collider.enabled = false;
        }

        private void Move()
        {
            if(m_tween is { active: true }) { m_tween.Kill(); }
            m_tween = transform.DOMove(transform.position + m_paperStack.PaperOffset, m_shadowMoveDuration).SetEase(m_shadowMoveEase);
        }
    }
}
