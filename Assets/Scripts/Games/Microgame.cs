using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IP1
{
    public class Microgame : MonoBehaviour
    {
        [SerializeField] private string m_nextSceneName;
        [SerializeField] private float m_nextGameWaitTime = 1.0f;

        private bool m_cleared;

        public Action OnCleared;
        public Action OnNextGame;

        private void Start()
        {
            OnCleared += () => { StartCoroutine(WaitForNextGame()); };
        }

        public void Clear()
        {
            if(m_cleared) { return; }
            OnCleared?.Invoke();
            m_cleared = true;
        }

        private IEnumerator WaitForNextGame()
        {
            if(m_nextGameWaitTime > 0.001f) { yield return new WaitForSeconds(m_nextGameWaitTime); }
            OnNextGame?.Invoke();
            SceneManager.LoadScene(m_nextSceneName);
        }
    }
}