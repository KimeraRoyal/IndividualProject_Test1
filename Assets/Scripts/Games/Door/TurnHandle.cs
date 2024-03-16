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

        [SerializeField] private Transform m_holdPointIndicator;

        [SerializeField] private Vector2 m_handleHoldPointSpeed = Vector2.one;

        [SerializeField] private float m_angle;

        private bool m_handleHeld;
        private Vector3 m_handleHoldPoint;
        
        private void Awake()
        {
            m_handle = FindObjectOfType<Handle>();

            m_mouseClick = GetComponent<MouseClick>();
            m_mouseMovement = GetComponent<MouseMovement>();
        }

        private void Start()
        {
            m_mouseClick.OnClickingChanged += OnClickingChanged;
            m_mouseClick.OnInteractableClicked += OnInteractableClicked;
            
            m_mouseMovement.OnMouseMoved += OnMouseMoved;
        }

        private void OnMouseMoved(Vector2 _mouseDelta)
        {
            if (!m_handleHeld) { return; }

            m_handleHoldPoint += new Vector3(_mouseDelta.x * m_handleHoldPointSpeed.x, _mouseDelta.y * m_handleHoldPointSpeed.y, 0);
        }

        private void Update()
        {
            CalculateAngle();
        }

        private void CalculateAngle()
        {
            if (!m_handleHeld) { return; }

            m_angle = -90 + Vector3.SignedAngle(m_handle.transform.position, m_handleHoldPoint, Vector3.forward);
            m_holdPointIndicator.transform.position = m_handleHoldPoint;
            
            m_handle.SetAngle(m_angle);
        }

        private void OnClickingChanged(bool _clicking)
        {
            if(_clicking || !m_handleHeld) { return; }

            m_handleHeld = false;
            
            m_handleHoldPoint = Vector3.zero;
        }

        private void OnInteractableClicked(RaycastHit _rayHit)
        {
            m_handleHeld = true;
            
            m_handleHoldPoint = _rayHit.point;
            m_handleHoldPoint.y = m_handle.transform.position.y;
        }
    }
}
