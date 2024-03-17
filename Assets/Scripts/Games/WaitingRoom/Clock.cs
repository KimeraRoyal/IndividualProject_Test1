using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private int m_maxTick = 216000;
        [SerializeField] private float m_speed = 1.0f;
        
        private int m_ticks;
        private float m_timer;

        public int CurrentTick => m_ticks;

        public Action<int> OnTick;

        private void Awake()
        {
            m_ticks = Random.Range(0, m_maxTick);
        }

        private void Update()
        {
            m_timer += Time.deltaTime * m_speed;
            if(m_timer < 1.0f) { return; }

            m_timer -= 1.0f;
            m_ticks = (m_ticks + 1) % m_maxTick;

            OnTick?.Invoke(++m_ticks);
        }
    }
}
