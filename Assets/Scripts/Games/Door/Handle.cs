using System;
using UnityEngine;

namespace IP1
{
    public class Handle : MonoBehaviour
    {
        private Microgame m_microgame;
        
        [SerializeField] private Transform m_gripPoint;
        
        [SerializeField] private float m_minAngle = -180;
        [SerializeField] private float m_maxAngle = 180;

        [SerializeField] private float m_minAngleUnlocked = -180;
        [SerializeField] private float m_maxAngleUnlocked = 180;

        [SerializeField] private float m_angleToUnlock;
        [SerializeField] private float m_angleToOpen;

        [SerializeField] private float m_angleSmoothing = 0.3f;

        private float m_angle;
        private float m_angleTarget;
        private float m_angleVelocity;

        private bool m_locked = true;
        private bool m_open;

        public Action OnUnlocked;
        public Action OnOpened;

        public Transform GripPoint => m_gripPoint;

        private void Awake()
        {
            m_microgame = GetComponentInParent<Microgame>();
        }

        private void Start()
        {
            OnOpened += m_microgame.Clear;
        }

        private void Update()
        {
            m_angle = Mathf.SmoothDamp(m_angle, m_angleTarget, ref m_angleVelocity, m_angleSmoothing);
            transform.localEulerAngles = Vector3.forward * m_angle;
            
            CheckUnlock();
            CheckOpen();
        }

        private void CheckUnlock()
        {
            if(!m_locked) { return; }
            
            if(m_angle < m_angleToUnlock) { return; }

            m_locked = false;
            OnUnlocked?.Invoke();
        }

        private void CheckOpen()
        {
            if (m_locked || m_open) { return; }
            
            if(m_angle > m_angleToOpen) { return; }

            m_open = true;
            OnOpened?.Invoke();

            m_angleTarget = 0;
        }

        // TODO: With how this class and TurnHandle work, the angle will snap if you hold the handle up and then trace a circle clockwise. This implies a different approach is needed.
        // ^ Perhaps perform changes in terms of how the angle changes per-frame, with a special edge case for passing the "snap" border between 180 and -180?
        public void SetAngle(float _angle)
        {
            var minAngle = m_locked ? m_minAngle : m_minAngleUnlocked;
            var maxAngle = m_locked ? m_maxAngle : m_maxAngleUnlocked;
            if (_angle > 180) { _angle = _angle - 360; }
            _angle = Mathf.Clamp(_angle, minAngle, maxAngle);
            
            m_angleTarget = _angle;
        }
    }
}
