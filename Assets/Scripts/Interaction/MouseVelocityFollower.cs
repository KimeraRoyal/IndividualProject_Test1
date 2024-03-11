using UnityEngine;

namespace IP1
{
    public class MouseVelocityFollower : MonoBehaviour
    {
        private Camera m_camera;
        
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        private Vector3 m_lastMouseWorldPosition;

        private void Awake()
        {
            m_camera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            if (!m_xFollow && !m_yFollow) { return; }
            
            var mousePosition = Input.mousePosition;
            mousePosition.z = m_camera.farClipPlane;
            
            var mouseWorldPosition = m_camera.ScreenToWorldPoint(mousePosition);
            if (!m_xFollow) { mouseWorldPosition.x = transform.position.x; }
            if (!m_yFollow) { mouseWorldPosition.y = transform.position.y; }
            mouseWorldPosition.z = 0;

            var difference = m_lastMouseWorldPosition - mouseWorldPosition;

            m_lastMouseWorldPosition = mouseWorldPosition;
        }
    }
}
