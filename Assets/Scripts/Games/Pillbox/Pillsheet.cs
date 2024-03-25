using System;
using UnityEngine;

namespace IP1
{
    public class Pillsheet : MonoBehaviour
    {
        private Microgame m_microgame;
        
        [SerializeField] private float m_dropLevelY;

        private bool m_dropped;

        private void Awake()
        {
            m_microgame = GetComponentInParent<Microgame>();
        }

        private void Update()
        {
            if(m_dropped || transform.position.y > m_dropLevelY) { return; }
            m_microgame.Clear();
        }
    }
}
