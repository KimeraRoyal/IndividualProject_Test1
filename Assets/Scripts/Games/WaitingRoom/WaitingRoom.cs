using System;
using UnityEngine;

namespace IP1
{
    public class WaitingRoom : MonoBehaviour
    {
        private Clock m_clock;

        [SerializeField] private int m_waitForTicks = 1;

        private int m_tickCount;

        private Action OnWaitComplete;

        private void Awake()
        {
            m_clock = FindObjectOfType<Clock>();
        }

        private void Start()
        {
            m_clock.OnTick += OnTick;

            OnWaitComplete += () => { Debug.Log("Waited!"); };
        }

        private void OnTick(int _ticks)
        {
            if (m_tickCount >= m_waitForTicks) { return; }
            
            m_tickCount++;
            
            if(m_tickCount < m_waitForTicks) { return; }
            
            OnWaitComplete?.Invoke();
        }
    }
}
