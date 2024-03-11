using UnityEngine;

namespace IP1.Interaction
{
    public class MouseVelocityFollower : MonoBehaviour
    {
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        [SerializeField] private float m_speed = 1.0f;

        private void Update()
        {
            if (!m_xFollow && !m_yFollow) { return; }
            
            var movement = new Vector3();
            if (m_xFollow) { movement.x = Input.GetAxis("Mouse X"); }
            if (m_yFollow) { movement.x = Input.GetAxis("Mouse Y"); }
            
            transform.position += movement * m_speed;
        }
    }
}
