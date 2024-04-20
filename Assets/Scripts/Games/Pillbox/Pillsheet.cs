using FMODUnity;
using UnityEngine;

namespace IP1
{
    public class Pillsheet : MonoBehaviour
    {
        private Microgame m_microgame;

        [SerializeField] private float m_fallSoundLevelY;
        [SerializeField] private float m_dropLevelY;

        [SerializeField] private EventReference m_fallEvent;

        private bool m_fallen;
        private bool m_dropped;

        private void Awake()
        {
            m_microgame = GetComponentInParent<Microgame>();
        }

        private void Update()
        {
            Fall();
            Drop();
        }

        private void Fall()
        {
            if(m_fallen || transform.position.y > m_fallSoundLevelY) { return; }

            RuntimeManager.PlayOneShot(m_fallEvent);
            m_fallen = true;
        }

        private void Drop()
        {
            if(m_dropped || transform.position.y > m_dropLevelY) { return; }
            
            m_microgame.Clear();
            m_dropped = true;
        }
    }
}
