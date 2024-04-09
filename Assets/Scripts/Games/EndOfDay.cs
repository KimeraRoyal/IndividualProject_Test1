using UnityEngine;

namespace IP1
{
    public class EndOfDay : MonoBehaviour
    {
        private GameState m_state;
        private Microgame m_microgame;

        private void Awake()
        {
            m_state = GetComponentInParent<GameState>();
            m_microgame = GetComponentInParent<Microgame>();
            
            if(!m_state) { return; }
            m_microgame.OnCleared += m_state.EndOfDay;
        }
    }
}
