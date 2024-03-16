using System;
using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(MouseClick), typeof(MouseMovement))]
    public class TurnHandle : MonoBehaviour
    {
        private Handle m_handle;

        private MouseClick m_mouseClick;
        private MouseMovement m_mouseMovement;

        [SerializeField] private Vector2 m_handleHoldPointSpeed = Vector2.one;

        private bool m_handleHeld;
        private Vector2 m_handleHoldPoint;

        private bool m_open;
        
        private void Awake()
        {
            m_handle = FindObjectOfType<Handle>();

            m_mouseClick = GetComponent<MouseClick>();
            m_mouseMovement = GetComponent<MouseMovement>();
        }

        private void Start()
        {
            m_handleHoldPoint = (Vector2) m_handle.transform.position + Vector2.left;
            
            m_mouseClick.OnClickingChanged += OnClickingChanged;
            m_mouseClick.OnInteractableClicked += OnInteractableClicked;
            
            m_mouseMovement.OnMouseMoved += OnMouseMoved;
            
            m_handle.OnOpened += () =>
            {
                m_open = true;
                m_mouseClick.Enabled = false;
            };
        }

        private void OnMouseMoved(Vector2 _mouseDelta)
        {
            if (!m_handleHeld) { return; }

            m_handleHoldPoint += new Vector2(_mouseDelta.x * m_handleHoldPointSpeed.x, _mouseDelta.y * m_handleHoldPointSpeed.y);
        }

        private void Update()
        {
            if (m_open) { return; }
            
            m_handle.SetAngle(180 - CalculateAngleAroundHandle(m_handle.transform.position, m_handleHoldPoint));
        }

        private void OnClickingChanged(bool _clicking)
        {
            if(_clicking || !m_handleHeld) { return; }

            m_handleHeld = false;
            m_handleHoldPoint = (Vector2) m_handle.transform.position + Vector2.left;
        }

        private void OnInteractableClicked(RaycastHit _rayHit)
        {
            m_handleHeld = true;
            m_handleHoldPoint = _rayHit.point;
            m_handleHoldPoint.y = m_handle.transform.position.y;
        }

        private static float CalculateAngleAroundHandle(Vector2 _center, Vector2 _point)
            => Mathf.Atan2(_point.y - _center.y, _point.x - _center.x) * Mathf.Rad2Deg;
    }
}
