using UnityEngine;

namespace IP1.Interaction
{
    public class MouseVelocityFollower : Mover
    {
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        [SerializeField] private Vector2 m_speed = Vector2.one;

        protected override void Update()
        {
            if (!m_xFollow && !m_yFollow) { return; }
            
            var movement = new Vector3();
            if (m_xFollow) { movement.x = Input.GetAxis("Mouse X"); }
            if (m_yFollow) { movement.y = Input.GetAxis("Mouse Y"); }

            var velocity = new Vector2(movement.x * m_speed.x, movement.y * m_speed.y);
            TargetPosition += velocity;
            
            base.Update();
        }
    }
}
