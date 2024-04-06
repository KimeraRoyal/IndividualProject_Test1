using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(Mover), typeof(Rotator), typeof(MouseMovement))]
    public class OpenPalm : MonoBehaviour
    {
        private Mover m_mover;
        private Rotator m_rotator;
        
        private MouseMovement m_mouseMovement;

        [SerializeField] private float m_moveInSpeed = 1.0f;
        [SerializeField] private float m_moveOutSpeed = 1.0f;

        [SerializeField] private float m_rotateArmSpeed = 1.0f;
        
        private void Awake()
        {
            m_mover = GetComponent<Mover>();
            m_rotator = GetComponent<Rotator>();
            
            m_mouseMovement = GetComponent<MouseMovement>();
        }

        private void Start()
        {
            m_mouseMovement.OnMouseMoved += OnMouseMoved;
        }

        private void OnMouseMoved(Vector2 _movement)
        {
            var speed = _movement.y > 0 ? m_moveOutSpeed : m_moveInSpeed;
            m_mover.TargetPosition += Vector3.forward * (_movement.y * speed);
            m_rotator.TargetRotation += Vector3.forward * (-_movement.x * m_rotateArmSpeed);
        }
    }
}
