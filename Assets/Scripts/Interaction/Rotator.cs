using System;
using UnityEngine;

namespace IP1
{
    public class Rotator : MonoBehaviour
    {
        public Func<Vector3, Vector3> OnRotateTarget;
        public Func<Vector3, Vector3> OnRotate;

        [SerializeField] private bool m_enabled = true;

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
            Debug.Log(transform.localEulerAngles);
            m_targetRotation = transform.localEulerAngles;
        }

        protected virtual void Start()
        {
            m_rotation = m_targetRotation;
            transform.localEulerAngles = m_targetRotation;
            Debug.Log(transform.localEulerAngles);
        }

        protected virtual void Update()
        {
            if(!m_enabled) { return; }
            
            var newRotation = Vector3.SmoothDamp(m_rotation, m_targetRotation, ref m_velocity, m_movementSmoothing);
            if (OnRotate != null)
            {
                newRotation = OnRotate.Invoke(newRotation);
            }
            m_rotation = newRotation;

            transform.localEulerAngles = m_rotation;
        }
    }
}
