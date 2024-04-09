using System;
using UnityEngine;

namespace IP1
{
    public class Rotator : MonoBehaviour
    {
        public Func<Vector3, Vector3> OnRotateTarget;
        public Func<Vector3, Vector3> OnRotate;

        [SerializeField] private bool m_enabled = true;

        [SerializeField] private float m_minRotationBound = -180.0f, m_maxRotationBound = 180.0f;

        [SerializeField] private float m_movementSmoothing = 0.1f;

        private Vector3 m_targetRotation;
        private Vector3 m_rotation;
        private Vector3 m_velocity;
        
        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        public Vector3 TargetRotation
        {
            get => m_targetRotation;
            set
            {
                m_targetRotation = value;
                if (OnRotateTarget != null)
                {
                    m_targetRotation = OnRotateTarget.Invoke(m_targetRotation);
                }
            }
        }

        protected virtual void Awake()
        {
            m_targetRotation = transform.localEulerAngles;
            for (var i = 0; i < 3; i++)
            {
                var bounds = (m_maxRotationBound - m_minRotationBound);
                
                m_targetRotation[i] %= bounds;
                if (m_targetRotation[i] > m_maxRotationBound) { m_targetRotation[i] -= bounds; }
                if (m_targetRotation[i] < m_minRotationBound) { m_targetRotation[i] += bounds; }
            }
        }

        protected virtual void Start()
        {
            m_rotation = m_targetRotation;
            transform.localEulerAngles = m_targetRotation;
        }

        protected virtual void Update()
        {
            if(!m_enabled || !enabled) { return; }
            
            var newRotation = Vector3.SmoothDamp(m_rotation, m_targetRotation, ref m_velocity, m_movementSmoothing);
            if (OnRotate != null)
            {
                newRotation = OnRotate.Invoke(newRotation);
            }
            m_rotation = newRotation;

            ClampToBounds();
            transform.localEulerAngles = m_rotation;
        }

        private void ClampToBounds()
        {
            for (var i = 0; i < 3; i++)
            {
                var bounds = (m_maxRotationBound - m_minRotationBound);

                if (m_rotation[i] > m_maxRotationBound)
                {
                    m_rotation[i] -= bounds;
                    m_targetRotation[i] -= bounds;
                }

                if (m_rotation[i] < m_minRotationBound)
                {
                    m_rotation[i] += bounds;
                    m_targetRotation[i] += bounds;
                }
            }
        }
    }
}
