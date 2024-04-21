using System;
using FMODUnity;
using UnityEngine;

namespace IP1
{
    public class Handle : MonoBehaviour
    {
        private enum AngleState
        {
            Resting,
            Up,
            Down
        }
        
        private GameState m_state;
        
        private Microgame m_microgame;

        [SerializeField] private Transform m_gripPoint;
        
        [SerializeField] private float m_minAngle = -180;
        [SerializeField] private float m_maxAngle = 180;

        [SerializeField] private float m_minAngleUnlocked = -180;
        [SerializeField] private float m_maxAngleUnlocked = 180;

        [SerializeField] private float m_angleToUnlock;
        [SerializeField] private float m_angleToOpen;

        [SerializeField] private float m_angleSmoothing = 0.3f;

        [SerializeField] private EventReference m_handleUpEvent, m_handleDownEvent;
        [SerializeField] private EventReference m_handleUpEventUnlocked, m_handleDownEventUnlocked;
        
        [SerializeField] private AngleState m_angleState;
        [SerializeField] private float m_minAngleSoundOffset = 10, m_minAngleSoundOffsetUnlocked = 10;
        [SerializeField] private float m_maxAngleSoundOffset = 10, m_maxAngleSoundOffsetUnlocked = 10;
        
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
            m_state = GetComponentInParent<GameState>();
            
            m_microgame = GetComponentInParent<Microgame>();
        }

        private void Start()
        {
            if(m_state) { OnOpened += m_state.BecomeHungry; }
            OnOpened += m_microgame.Clear;
        }

        private void Update()
        {
            m_angle = Mathf.SmoothDamp(m_angle, m_angleTarget, ref m_angleVelocity, m_angleSmoothing);
            transform.localEulerAngles = Vector3.forward * m_angle;

            CheckAngleState();
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

        private void CheckAngleState()
        {
            var minAngleOffset = m_locked ? m_minAngleSoundOffset : m_minAngleSoundOffsetUnlocked;
            var maxAngleOffset = m_locked ? m_maxAngleSoundOffset : m_maxAngleSoundOffsetUnlocked;
            
            var minAngle = (m_locked ? m_minAngle : m_minAngleUnlocked) + minAngleOffset;
            var maxAngle = (m_locked ? m_maxAngle : m_maxAngleUnlocked) - maxAngleOffset;

            var angleState = AngleState.Resting;
            if (m_angle > maxAngle) { angleState = AngleState.Up; }
            else if (m_angle < minAngle) { angleState = AngleState.Down; }

            if (angleState != m_angleState)
            {
                switch (angleState)
                {
                    case AngleState.Up:
                    {
                        RuntimeManager.PlayOneShot(m_locked ? m_handleUpEvent : m_handleUpEventUnlocked);
                        break;
                    }
                    case AngleState.Down:
                    {
                        RuntimeManager.PlayOneShot(m_locked ? m_handleDownEvent : m_handleDownEventUnlocked);
                        break;
                    }
                    case AngleState.Resting:
                    {
                        break;
                    }
                }
            }
            
            m_angleState = angleState;
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
