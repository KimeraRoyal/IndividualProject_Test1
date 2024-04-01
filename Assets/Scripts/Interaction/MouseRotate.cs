using UnityEngine;

namespace IP1
{
    public class MouseRotate : Rotator
    {
        [SerializeField] private Vector2 m_speed;

        protected override void Update()
        {
            if (!Enabled) { return; }
            
            var movement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

            var velocity = new Vector3(-movement.y * m_speed.y, movement.x * m_speed.x);
            TargetRotation += velocity;
            
            base.Update();
        }
    }
}
