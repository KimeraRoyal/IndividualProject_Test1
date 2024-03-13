using System;
using UnityEngine;

namespace IP1.Interaction
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MouseRigidbodyVelocity : MonoBehaviour
    {
        private Rigidbody2D m_rigidbody;
        
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        [SerializeField] private Vector2 m_speed = Vector2.one;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!m_xFollow && !m_yFollow) { return; }
            
            var movement = new Vector3();
            if (m_xFollow) { movement.x = Input.GetAxis("Mouse X"); }
            if (m_yFollow) { movement.y = Input.GetAxis("Mouse Y"); }
            
            var velocity = new Vector3(movement.x * m_speed.x, movement.y * m_speed.y, 0.0f);
            
            //m_rigidbody.AddForce(velocity, ForceMode2D.Impulse);
            m_rigidbody.velocity = Vector3.Lerp(velocity, m_rigidbody.velocity, 0.8f);
        }
    }
}
