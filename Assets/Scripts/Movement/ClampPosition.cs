using UnityEngine;

namespace IP1.Movement
{
    public class ClampPosition : MonoBehaviour
    {
        private Rigidbody2D m_rigidbody;

        [SerializeField] private bool m_xClampToBounds;
        [SerializeField] private bool m_yClampToBounds;
        
        [SerializeField] private Vector2 m_minBounds, m_maxBounds;

        [SerializeField] private bool m_useLocalPosition;
        
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (m_xClampToBounds) { ClampAxis(0); }
            if (m_yClampToBounds) { ClampAxis(1); }
        }

        private void ClampAxis(int _axis)
        {
            var position = m_useLocalPosition ? transform.localPosition : transform.position;
            
            var clamped = false;
            
            if (position[_axis] < m_minBounds[_axis])
            {
                position[_axis] = m_minBounds[_axis];
                clamped = true;
            }
            if (position[_axis] > m_maxBounds[_axis])
            { 
                position[_axis] = m_maxBounds[_axis];
                clamped = true;
            }
                
            if (m_rigidbody && clamped)
            {
                var velocity = m_rigidbody.velocity;
                velocity[_axis] = 0.0f;
                m_rigidbody.velocity = velocity;
            }
            
            if(m_useLocalPosition) { transform.localPosition = position; }
            else { transform.position = position; }
        }
    }
}
