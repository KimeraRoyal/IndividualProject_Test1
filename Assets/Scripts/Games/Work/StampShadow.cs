using System;
using IP1.Interaction;
using IP1.Movement;
using UnityEngine;

namespace IP1
{
    public class StampShadow : MonoBehaviour
    {
        private PaperStack m_paperStack;

        private Mover m_mover;

        private ClampPosition m_clampPosition;

        [SerializeField] private int m_interactableLayerId;
        
        [SerializeField] private LayerMask m_boxcastLayerMask;
        [SerializeField] private Vector2 m_boxcastOrigin;
        [SerializeField] private Vector2 m_boxcastSize = Vector2.one;

        public Action OnRise;

        private void Awake()
        {
            m_paperStack = FindObjectOfType<PaperStack>();

            m_mover = GetComponent<Mover>();
            m_clampPosition = GetComponent<ClampPosition>();
            
            m_paperStack.OnPaperAdded += OnPaperAdded;
        }

        private void Update()
        {
            var rayHit = Physics2D.BoxCast(m_boxcastOrigin, m_boxcastSize, 0, Vector2.zero, 0, m_boxcastLayerMask);
            if(rayHit.collider == null) { return; }
            
            Move();

            rayHit.collider.gameObject.layer = m_interactableLayerId;
        }

        private void Move()
        {
            m_mover.TargetPosition += m_paperStack.PaperOffset;
            OnRise?.Invoke();
        }

        private void OnPaperAdded(Paper _paper)
        {
            var offset = Vector3.up * m_paperStack.PaperOffset.y;
            m_clampPosition.MinBounds += offset;
            m_clampPosition.MaxBounds += offset;
        }
    }
}
