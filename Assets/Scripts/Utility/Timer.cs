using System;
using UnityEngine;

namespace IP1
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float m_incrementTime = 1.0f;

        private float m_currentTime;

        public Action OnTick;

        private void Update()
        {
            m_currentTime += Time.deltaTime;
            if(m_currentTime < m_incrementTime) { return; }
            m_currentTime -= m_incrementTime;

            OnTick?.Invoke();
        }
    }
}
