using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class Mover : MonoBehaviour
    {
        public Func<Vector3, Vector3> OnMoveTarget;
        public Func<Vector3, Vector3> OnMove;

        [SerializeField] private bool m_enabled = true;

        [SerializeField] private float m_movementSmoothing = 0.1f;

        private Vector3 m_targetPosition;
        private Vector3 m_velocity;

        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        public Vector3 TargetPosition
        {
            get => m_targetPosition;
            set
            {
                m_targetPosition = value;
                if (OnMoveTarget != null)
                {
                    m_targetPosition = OnMoveTarget.Invoke(m_targetPosition);
                }
            }
        }

        protected virtual void Start()
        {
            m_targetPosition = transform.localPosition;
        }

        protected virtual void Update()
        {
            if(!m_enabled) { return; }
            
            var localTransform = transform;
            
            var newLocalPosition = Vector3.SmoothDamp(localTransform.localPosition, m_targetPosition, ref m_velocity, m_movementSmoothing);
            if (OnMove != null)
            {
                newLocalPosition = OnMove.Invoke(newLocalPosition);
            }
            transform.localPosition = newLocalPosition;
        }
    }
}