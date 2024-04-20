using System;
using FMODUnity;
using UnityEngine;

namespace IP1
{
    public class RightHand : MonoBehaviour
    {
        [SerializeField] private LayerMask m_interactableLayerMask;
        [SerializeField] private Transform m_interactionPoint;
        [SerializeField] private float m_interactionCastRadius = 1.0f;
        
        [SerializeField] private float m_distanceDropoff = 0.1f;

        [SerializeField] private EventReference m_grabSheetEvent;
        [SerializeField] private EventReference m_dropSheetEvent;
        [SerializeField] private EventReference m_jostleSheetEvent;
        
        private bool m_gripping;

        private Vector3 m_lastPosition;
        private Vector2 m_playerMovement;

        private float m_averageDistance;
        private float m_distance;
        private bool m_distanceChanged;

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
            if (rayHit.collider)
            {
                Jostle();
                return;
            }

            Gripping = false;
            m_pillSheet = null;
        }

        private void Jostle()
        {
            var movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;
            
            m_distance = Vector2.Distance(movement, m_playerMovement);

            var jostled = false;
            if (m_averageDistance < 0.00001f)
            {
                m_averageDistance = m_distance; 
            }
            else
            {
                m_averageDistance = Mathf.Lerp(m_averageDistance, m_distance, m_distanceDropoff);
            }

            if (m_averageDistance > 0.01f)
            {
                if (!m_distanceChanged)
                {
                    jostled = true;
                    m_distanceChanged = true;
                }
            }
            else
            {
                m_distanceChanged = false;
            }

            if (jostled) { RuntimeManager.PlayOneShot(m_jostleSheetEvent); }
            
            m_playerMovement = movement;
        }

        private void GripChange(bool _gripping)
        {
            if (_gripping)
            {
                var rayHit = Physics2D.CircleCast(m_interactionPoint.position, m_interactionCastRadius, Vector2.up, 0, m_interactableLayerMask);
                Grab(rayHit.transform);
            }
            else
            {
                if (m_pillSheet) { Release(); }
            }
        }

        private void Grab(Transform _hit)
        {
            if(!_hit) { return; }
            
            m_pillSheet = _hit;
            RuntimeManager.PlayOneShot(m_grabSheetEvent);
        }

        private void Release()
        {
            m_pillSheet = null;
            if(Input.GetMouseButtonUp(0)) { RuntimeManager.PlayOneShot(m_dropSheetEvent); }
        }
    }
}
