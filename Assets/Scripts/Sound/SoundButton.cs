using FMODUnity;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(FaceButton))]
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private EventReference m_pressedEvent;
        [SerializeField] private EventReference m_unpressedEvent;

        [SerializeField] private float m_waitTime;
        
        private float m_timer;
        private bool m_unpressedDirty;
        
        private void Awake()
        {
            GetComponent<FaceButton>().OnPressedChange += OnPressedChange;
        }

        private void Update()
        {
            if(m_unpressedEvent.IsNull || m_timer < 0.001f) { return; }

            m_timer -= Time.deltaTime;
            if(m_timer > 0.001f || !m_unpressedDirty) { return; }

            RuntimeManager.PlayOneShot(m_unpressedEvent);
            m_unpressedDirty = false;
        }

        private void OnPressedChange(bool _pressed)
        {
            var oneShot = _pressed ? m_pressedEvent : m_unpressedEvent;
            if(oneShot.IsNull) { return; }

            if (_pressed && m_waitTime > 0.001f)
            {
                m_timer = m_waitTime;
            }
            
            if (!_pressed && m_timer > 0.001f)
            {
                m_unpressedDirty = true;
            }
            else
            {
                RuntimeManager.PlayOneShot(oneShot);
            }
        }
    }
}
