using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class MouseFollower : MonoBehaviour
    {
        private Camera m_camera;

        [SerializeField] private bool m_enabled = true;
        
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        [SerializeField] private float m_movementSmoothing = 0.1f;
        
        private Vector3 m_targetPosition;
        private Vector3 m_movementVelocity;

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
                if (m_enabled) { return; }
                m_targetPosition = value;
            }
        }

        private void Awake()
        {
            m_camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            m_targetPosition = transform.localPosition;
        }

        private void Update()
        {
            CalculatePosition();

            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, m_targetPosition, ref m_movementVelocity, m_movementSmoothing);
        }

        private void CalculatePosition()
        {
            if (!m_enabled || (!m_xFollow && !m_yFollow)) { return; }

            var z = transform.position.z;
            var zOffset = z - m_camera.transform.position.z;
            if (Mathf.Abs(zOffset) < 0.001f) { zOffset = m_camera.farClipPlane; }
            
            var mousePosition = Input.mousePosition;
            mousePosition.z = zOffset;
            
            var mouseWorldPosition = m_camera.ScreenToWorldPoint(mousePosition);
            if (!m_xFollow) { mouseWorldPosition.x = transform.localPosition.x; }
            if (!m_yFollow) { mouseWorldPosition.y = transform.localPosition.y; }
            mouseWorldPosition.z = z;

            m_targetPosition = mouseWorldPosition;
        }
    }
}
