using FMODUnity;
using UnityEngine;

namespace IP1
{
    public class SoundVelocity : MonoBehaviour
    {
        [SerializeField] private float m_minVelocity = 1.0f;
        [SerializeField] private float m_velocityFalloff = 1.0f;

        [SerializeField] private Vector3 m_axisFactors = Vector3.one;

        [SerializeField] private EventReference m_soundEvent;
        
        private float m_averageVelocity;
        
        private Vector3 m_previousPosition;

        private bool m_played;

        private void Start()
        {
            m_previousPosition = transform.position;
            m_previousPosition = new Vector3(m_previousPosition.x * m_axisFactors.x, m_previousPosition.y * m_axisFactors.y, m_previousPosition.z * m_axisFactors.z);
        }

        private void Update()
        {
            var position = transform.position;
            position = new Vector3(position.x * m_axisFactors.x, position.y * m_axisFactors.y, position.z * m_axisFactors.z);

            var velocity = (position - m_previousPosition).magnitude;
            if (m_averageVelocity < 0.001f)
            {
                m_averageVelocity = velocity;
            }
            else
            {
                m_averageVelocity = Mathf.Lerp(m_averageVelocity, velocity, m_velocityFalloff);
            }

            if (m_averageVelocity > m_minVelocity)
            {
                if (!m_played)
                {
                    RuntimeManager.PlayOneShot(m_soundEvent);
                    m_played = true;
                }
            }
            else
            {
                m_played = false;
            }

            m_previousPosition = position;
        }
    }
}
