using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IP1
{
    [Serializable]
    public class GameState
    {
        [SerializeField] private string m_id;
        [SerializeField] private Microgame m_microgame;
        
        [SerializeField] private string m_nextGame;

        public string ID => m_id;

        public Microgame Microgame => m_microgame;

        public string NextGame => m_nextGame;
    }
    
    public class Games : MonoBehaviour
    {
        [SerializeField] private GameState[] m_states;

        [SerializeField] private float m_loadTime = 1.0f;

        private Dictionary<string, int> m_stateIndexReference;

        private int m_currentState = -1;
        private Microgame m_currentMicrogame;
        
        private void Awake()
        {
            m_stateIndexReference = new Dictionary<string, int>();
            for (var i = 0; i < m_states.Length; i++)
            {
                m_stateIndexReference.Add(m_states[i].ID, i);
            }
        }

        private void Start()
        {
            LoadState(0);
        }

        private void LoadState(int _id)
        {
            UnloadCurrent();
            
            m_currentState = _id;
            StartCoroutine(WaitAndLoad());
        }

        private IEnumerator WaitAndLoad()
        {
            yield return new WaitForSeconds(m_loadTime);
            
            m_currentMicrogame = Instantiate(m_states[m_currentState].Microgame, transform);
            m_currentMicrogame.OnNextGameRequested += OnNextGameRequested;
        }

        private void UnloadCurrent()
        {
            if (!m_currentMicrogame) { return; }
            m_currentMicrogame.OnNextGameRequested -= OnNextGameRequested;
            Destroy(m_currentMicrogame.gameObject);
        }

        private void OnNextGameRequested()
        {
            LoadState(m_stateIndexReference[m_states[m_currentState].NextGame]);
        }
    }
}
