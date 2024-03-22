using System.Collections;
using System.Collections.Generic;
using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    public class SplitMouseVelocityFollower : Mover
    {
        [SerializeField] private bool m_xFollow = true;
        [SerializeField] private bool m_yFollow = true;

        [SerializeField] private Vector2 m_backSpeed = Vector2.one;
        [SerializeField] private Vector2 m_forwardSpeed = Vector2.one;
        
        protected override void Update()
        {
            if (!m_xFollow && !m_yFollow) { return; }
            
            var movement = new Vector3();
            var speed = new Vector2();

            if (m_xFollow)
            {
                movement.x = Input.GetAxis("Mouse X");
                speed.x = movement.x > 0 ? m_forwardSpeed.x : m_backSpeed.x;
            }
            if (m_yFollow)
            {
                movement.y = Input.GetAxis("Mouse Y");
                speed.y = movement.y > 0 ? m_forwardSpeed.y : m_backSpeed.y;
            }

            var velocity = new Vector3(movement.x * speed.x, movement.y * speed.y);
            TargetPosition += velocity;
            
            base.Update();
        }
    }
}
