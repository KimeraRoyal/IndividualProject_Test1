using System;
using UnityEngine;

namespace IP1
{
    public class WaitingRoom : MonoBehaviour
    {
        private GameState m_state;
        private Microgame m_microgame;
        
        private Clock m_clock;

        [SerializeField] private int m_waitForTicks = 1;

        private int m_tickCount;

        private Action OnWaitComplete;

        private void Awake()
        {
            m_state = GetComponentInParent<GameState>();
            m_microgame = GetComponentInParent<Microgame>();
            
            m_clock = FindObjectOfType<Clock>();
        }

        private void Start()
        {
            m_clock.OnTick += OnTick;

            OnWaitComplete += m_microgame.Clear;
            if(m_state) { OnWaitComplete += m_state.Refill; }
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
