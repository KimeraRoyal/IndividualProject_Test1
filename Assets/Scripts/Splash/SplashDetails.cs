using UnityEngine;

namespace IP1
{
    [CreateAssetMenu(fileName = "Splash Details", menuName = "Splash/Splash Details")]
    public class SplashDetails : ScriptableObject
    {
        [SerializeField] private string m_title;
        [SerializeField] private string m_subtitle;

        public string GetTitle()
            => m_title;
        
        public string GetSubtitle(int _prescription, int _sheetsRemaining)
            => string.Format(m_subtitle, _prescription, _sheetsRemaining);
    }
}
