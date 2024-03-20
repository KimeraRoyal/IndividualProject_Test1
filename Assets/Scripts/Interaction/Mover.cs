using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace IP1.Interaction
{
    public class Mover : MonoBehaviour
    {
        public Func<Vector3, Vector3> OnMoveTargetPosition;
        public Func<Vector3, Vector3> OnMove;

        [SerializeField] private float m_movementSmoothing = 0.1f;

        private Vector3 m_targetPosition;
        private Vector3 m_velocity;

        public Vector2 TargetPosition
        {
            get => m_targetPosition;
            set
            {
                m_targetPosition = value;
                if (OnMoveTargetPosition != null)
                {
                    m_targetPosition = OnMoveTargetPosition.Invoke(m_targetPosition);
                }
            }
        }

        protected virtual void Start()
        {
            m_targetPosition = transform.localPosition;
        }

        protected virtual void Update()
        {
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