using UnityEngine;

namespace IP1
{
    public class FadeOnMicrogameComplete : MonoBehaviour
    {
        private Microgame m_microgame;
        
        private FadeImageInOut[] m_faders;

        private float m_gradientProgression;

        private void Awake()
        {
            m_microgame = GetComponentInParent<Microgame>();

            m_faders = GetComponentsInChildren<FadeImageInOut>();
        }

        private void Start()
        {
            m_microgame.OnCleared += OnCleared;
        }

        private void OnCleared()
        {
            foreach (var fader in m_faders)
            {
                fader.Fade(true);
            }
        }
    }
}
