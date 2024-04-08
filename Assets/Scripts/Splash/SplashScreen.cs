using TMPro;
using UnityEngine;

namespace IP1
{
    public class SplashScreen : MonoBehaviour
    {
        private GameObject m_elements;
        private TMP_Text[] m_text;

        public bool Active
        {
            get => m_elements.activeSelf;
            set => m_elements.SetActive(value);
        }

        public string Title
        {
            get => m_text[0].text;
            set => m_text[0].text = value;
        }

        public string Subtitle
        {
            get => m_text[1].text;
            set => m_text[1].text = value;
        }

        private void Awake()
        {
            m_elements = transform.GetChild(0).gameObject;
            m_text = GetComponentsInChildren<TMP_Text>();
        }
    }
}
