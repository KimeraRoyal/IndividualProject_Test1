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

        public Vector2 MinBounds { get => m_minBounds; set => m_minBounds = value; }
        public Vector2 MaxBounds { get => m_maxBounds; set => m_maxBounds = value; }
        
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
            
            Clamp();
        }

        private void FixedUpdate()
        {
            Clamp();
        }

        private Vector3 Clamp(Vector3 _position)
        {
            if (m_xClampToBounds) { _position = ClampAxis(0, _position); }
            if (m_yClampToBounds) { _position = ClampAxis(1, _position); }

            return _position;
        }

        private void Clamp()
        {
            var position = m_useLocalPosition ? transform.localPosition : transform.position;

            position = Clamp(position);

            if (m_useLocalPosition)
            {
                transform.localPosition = position;
            }
            else
            {
                transform.position = position;
            }
        }

        private Vector3 ClampAxis(int _axis, Vector3 _position)
        {
            var clamped = false;

            var rebound = 0;
            if (_position[_axis] < m_minBounds[_axis])
            {
                _position[_axis] = m_minBounds[_axis];
                clamped = true;
                rebound = 1;
            }
            if (_position[_axis] > m_maxBounds[_axis])
            { 
                _position[_axis] = m_maxBounds[_axis];
                clamped = true;
                rebound = -1;
            }
                
            if (m_rigidbody && clamped)
            {
                var velocity = m_rigidbody.velocity;
                velocity[_axis] = rebound;
                m_rigidbody.velocity = velocity;
            }

            return _position;
        }
    }
}
