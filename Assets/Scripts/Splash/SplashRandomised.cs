using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Randomised Splash", menuName = "Splash/Randomised Splash")]
    public class SplashRandomised : SplashDetails
    {
        [SerializeField] private string[] m_titles;
        [SerializeField] private string[] m_subtitles;

        public override string GetTitle(GameState _state)
            => m_titles.Length > 0 ? m_titles[Random.Range(0, m_titles.Length)] : base.GetTitle(_state);

        public override string GetSubtitle(GameState _state)
            => m_subtitles.Length > 0 ? m_subtitles[Random.Range(0, m_subtitles.Length)] : base.GetSubtitle(_state);
    }
}
