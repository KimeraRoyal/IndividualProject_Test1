using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class Mover : MonoBehaviour
    {
        public Func<Vector3, Vector3> OnMoveTarget;
        public Func<Vector3, Vector3> OnMoveReal;
        public Func<Vector3, Vector3> OnMove;

        [SerializeField] private bool m_enabled = true;

        [SerializeField] private float m_movementSmoothing = 0.1f;

        private Vector3 m_realPosition;
        private Vector3 m_targetPosition;
        private Vector3 m_velocity;

        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        public Vector3 RealPosition
        {
            get => m_realPosition;
            set
            {
                m_realPosition = value;
                if (OnMoveReal != null)
                {
                    m_realPosition = OnMoveReal.Invoke(m_targetPosition);
                }
            }
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
            m_realPosition = m_targetPosition;
        }

        protected virtual void Update()
        {
            if(!m_enabled) { return; }

            RealPosition = Vector3.SmoothDamp(m_realPosition, m_targetPosition, ref m_velocity, m_movementSmoothing);
            
            var newLocalPosition = RealPosition;
            if (OnMove != null)
            {
                newLocalPosition = OnMove.Invoke(m_realPosition);
            }
            transform.localPosition = newLocalPosition;
        }
    }
}