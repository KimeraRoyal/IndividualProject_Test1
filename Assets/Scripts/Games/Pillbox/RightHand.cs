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

        public Action<bool> OnGrippingChanged;

        private void Awake()
        {
            OnGrippingChanged += GripChange;
        }

        private void Update()
        {
            CheckInput();
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

        private void CheckInput()
        {
            var gripping = Input.GetMouseButton(0);
            if (gripping == m_gripping) { return; }
            
            m_gripping = gripping;
            OnGrippingChanged?.Invoke(m_gripping);
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
