using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class MouseFollower : Mover
    {
        private Camera m_camera;

        [SerializeField] private bool m_enabled = true;
        
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        [SerializeField] private Vector3 m_offset;
        [SerializeField] private Vector3 m_scale = Vector3.one;

        public bool Enabled
        {
            get => m_enabled;
            set => m_enabled = value;
        }

        private void Awake()
        {
            m_camera = FindObjectOfType<Camera>();
        }

        protected override void Update()
        {
            CalculatePosition();

            base.Update();
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

            TargetPosition = new Vector3(mouseWorldPosition.x * m_scale.x, mouseWorldPosition.y * m_scale.y, mouseWorldPosition.z * m_scale.z) + m_offset;
        }
    }
}
