using System;
using UnityEngine;

namespace IP1.Games.Pillbox
{
    [RequireComponent(typeof(Animator))]
    public class RightHand : MonoBehaviour
    {
        private Animator m_animator;

        private bool m_gripping;

        [SerializeField] private string m_grippingParameterName = "Gripping";

        [SerializeField] private LayerMask m_interactableLayerMask;
        [SerializeField] private Transform m_interactionPoint;
        [SerializeField] private float m_interactionCastRadius = 1.0f;

        private Transform m_pillSheet;

        private Vector3 m_lastPosition;
        
        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var gripping = Input.GetMouseButton(0);
            if (gripping == m_gripping) { return; }
            
            m_gripping = gripping;
            OnGrippingChanged(m_gripping);
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

        private void OnGrippingChanged(bool _gripping)
        {
            m_animator.SetBool(m_grippingParameterName, _gripping);
            
            // TODO: Release object if it falls out of hands
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
