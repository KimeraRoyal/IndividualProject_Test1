using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Basic Splash", menuName = "Splash/Basic Splash")]
    public class SplashDetails : ScriptableObject
    {
        [SerializeField] private string m_title;
        [SerializeField] private string m_subtitle;

        public virtual string GetTitle(GameState _state)
            => m_title;
        
        public virtual string GetSubtitle(GameState _state)
            => m_subtitle;
    }
}
