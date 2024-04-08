using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IP1
{
    [Serializable]
    public class MicrogameInfo
    {
        [SerializeField] private string m_id;
        [SerializeField] private Microgame m_microgame;

        [SerializeField] private SplashDetails m_splashDetails;
        
        [SerializeField] private MicrogameSelector m_selector;

        public string ID => m_id;

        public Microgame Microgame => m_microgame;

        public SplashDetails SplashDetails => m_splashDetails;

        public string SelectNextGame(GameState _state)
            => m_selector.Select(_state);
    }
    
    public class Games : MonoBehaviour
    {
        private SplashScreen m_splashScreen;
        
        private GameState m_state;

        [SerializeField] private MicrogameInfo[] m_states;
        [SerializeField] private string m_firstGame;

        [SerializeField] private float m_loadTime = 1.0f;
        [SerializeField] private float m_splashTime = 1.0f;

        private Dictionary<string, int> m_stateIndexReference;

        private int m_currentState = -1;
        private Microgame m_currentMicrogame;
        
        private void Awake()
        {
            m_splashScreen = FindObjectOfType<SplashScreen>();
            
            m_state = GetComponent<GameState>();
            
            m_stateIndexReference = new Dictionary<string, int>();
            for (var i = 0; i < m_states.Length; i++)
            {
                m_stateIndexReference.Add(m_states[i].ID, i);
            }

            m_splashScreen.Active = false;
        }

        private void Start()
        {
            LoadState(m_stateIndexReference[m_firstGame]);
        }

        private void LoadState(int _id)
        {
            var time = m_currentState < 0 ? 0 : m_loadTime;
            
            UnloadCurrent();
            StartCoroutine(WaitAndLoad(time, _id));
        }

        private IEnumerator WaitAndLoad(float _waitTime, int _id)
        {
            if(_waitTime > 0.001f) { yield return new WaitForSeconds(m_loadTime); }

            if (m_states[_id].SplashDetails) { yield return ShowSplash(_id); }
            
            m_currentMicrogame = Instantiate(m_states[_id].Microgame, transform);
            m_currentMicrogame.OnNextGameRequested += OnNextGameRequested;
                
            m_currentState = _id;
        }

        private IEnumerator ShowSplash(int _id)
        {
            m_splashScreen.Active = true;
            m_splashScreen.Title = m_states[_id].SplashDetails.GetTitle();
            m_splashScreen.Subtitle = m_states[_id].SplashDetails.GetSubtitle(m_state.PrescriptionAmount * 2, m_state.SheetsRemaining + 1);

            yield return new WaitForSeconds(m_splashTime);
            m_splashScreen.Active = false;
        }

        private void UnloadCurrent()
        {
            if (!m_currentMicrogame) { return; }
            m_currentMicrogame.OnNextGameRequested -= OnNextGameRequested;
            Destroy(m_currentMicrogame.gameObject);
        }

        private void OnNextGameRequested()
        {
            LoadState(m_stateIndexReference[m_states[m_currentState].SelectNextGame(m_state)]);
        }
    }
}
