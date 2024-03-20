using System;
using IP1.Interaction;
using UnityEngine;

namespace IP1.Movement
{
    public class ClampPosition : MonoBehaviour
    {
        private Mover[] m_movers;
        private Rigidbody2D m_rigidbody;

        [SerializeField] private bool m_xClampToBounds;
        [SerializeField] private bool m_yClampToBounds;
        
        [SerializeField] private Vector2 m_minBounds, m_maxBounds;

        [SerializeField] private bool m_useLocalPosition;
        
        private void Awake()
        {
            m_movers = GetComponents<Mover>();
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (m_movers == null || m_movers.Length < 1) { return; }

            foreach (var mover in m_movers)
            {
                mover.OnMoveTargetPosition += Clamp;
                mover.OnMove += Clamp;
            }
        }

        private void FixedUpdate()
        {
            Clamp();
        }

        private void Clamp()
        {
            var position = m_useLocalPosition ? transform.localPosition : transform.position;
            
            if (m_xClampToBounds) { ClampAxis(0, position); }
            if (m_yClampToBounds) { ClampAxis(1, position); }

            if (m_useLocalPosition)
            {
                transform.localPosition = position;
            }
            else
            {
                transform.position = position;
            }
        }

        private Vector3 Clamp(Vector3 _position)
        {
            if (m_xClampToBounds) { _position = ClampAxis(0, _position); }
            if (m_yClampToBounds) { _position = ClampAxis(1, _position); }

            return _position;
        }

        private Vector3 ClampAxis(int _axis, Vector3 _position)
        {
            var clamped = false;
            
            if (_position[_axis] < m_minBounds[_axis])
            {
                _position[_axis] = m_minBounds[_axis];
                clamped = true;
            }
            if (_position[_axis] > m_maxBounds[_axis])
            { 
                _position[_axis] = m_maxBounds[_axis];
                clamped = true;
            }
                
            if (m_rigidbody && clamped)
            {
                var velocity = m_rigidbody.velocity;
                velocity[_axis] = 0.0f;
                m_rigidbody.velocity = velocity;
            }

            return _position;
        }
    }
}
