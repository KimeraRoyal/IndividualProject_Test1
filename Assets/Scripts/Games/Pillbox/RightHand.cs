using System;
using UnityEngine;

namespace IP1
{
    public class RightHand : MonoBehaviour
    {
        [SerializeField] private LayerMask m_interactableLayerMask;
        [SerializeField] private Transform m_interactionPoint;
        [SerializeField] private float m_interactionCastRadius = 1.0f;
        
        private bool m_gripping;

        private Vector3 m_lastPosition;

        private Transform m_pillSheet;

        public bool Gripping
        {
            get => m_gripping;
            set
            {
                if (value == m_gripping) { return; }
                m_gripping = value;
                OnGrippingChanged?.Invoke(m_gripping);
            }
        }

        public Action<bool> OnGrippingChanged;

        private void Awake()
        {
            OnGrippingChanged += GripChange;
        }

        private void Update()
        {
            Gripping = Input.GetMouseButton(0);
            Grip();
        }

        private void LateUpdate()
        {
            var position = transform.position;
            var distance = position - m_lastPosition;

            if (m_pillSheet)
            {
                m_pillSheet.transform.position += distance;
            }
            
            m_lastPosition = position;
        }

        private void Grip()
        {
            if(!m_gripping) { return; }
            
            var rayHit = Physics2D.CircleCast(m_interactionPoint.position, m_interactionCastRadius, Vector2.up, 0, m_interactableLayerMask);
            if(rayHit.collider) { return; }

            Gripping = false;
        }

        private void GripChange(bool _gripping)
        {
            if (_gripping)
            {
                var rayHit = Physics2D.CircleCast(m_interactionPoint.position, m_interactionCastRadius, Vector2.up, 0, m_interactableLayerMask);
                if (rayHit.transform)
                {
                    m_pillSheet = rayHit.transform;
                }
            }
            else
            {
                if (m_pillSheet) { m_pillSheet = null; }
            }
        }
    }
}
