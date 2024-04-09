using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(Microgame))]
    public class InstantWin : MonoBehaviour
    {
        private Microgame m_microgame;

        private void Awake()
        {
            m_microgame = GetComponent<Microgame>();
        }

        private void Start()
        {
            m_microgame.Clear();
        }
    }
}
