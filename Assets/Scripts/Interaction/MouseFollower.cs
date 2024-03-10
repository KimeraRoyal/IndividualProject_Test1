using UnityEngine;

namespace IP1.Interaction
{
    public class MouseFollower : MonoBehaviour
    {
        private Camera m_camera;

        private Vector2Int m_screenSize;
        private float m_aspectRatio;
        
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        private void Awake()
        {
            m_camera = FindObjectOfType<Camera>();

            m_screenSize = new Vector2Int(Screen.width, Screen.height);
            m_aspectRatio = (float) m_screenSize.x / m_screenSize.y;
        }

        private void Update()
        {
            if (!m_xFollow && !m_yFollow) { return; }
            
            var mousePosition = Input.mousePosition;
            mousePosition.z = m_camera.farClipPlane;
            
            var worldPosition = m_camera.ScreenToWorldPoint(mousePosition);
            if (!m_xFollow) { worldPosition.x = transform.position.x; }
            if (!m_yFollow) { worldPosition.y = transform.position.y; }
            worldPosition.z = 0;

            transform.position = worldPosition;
        }
    }
}
