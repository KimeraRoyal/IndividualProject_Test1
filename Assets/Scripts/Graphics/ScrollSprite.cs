using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace IP1
{
    [RequireComponent(typeof(SpriteRenderer))] [ExecuteInEditMode]
    public class ScrollSprite : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        [SerializeField] private Vector2 m_spriteSize = Vector2.one;
        
        [SerializeField] private Vector2 m_minSpeed, m_maxSpeed;

        private Vector2 m_tiling;
        private Vector2 m_speed;

        public Vector2 Speed
        {
            get => m_speed;
            set => m_speed = value;
        }

        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            m_tiling = new Vector2(Random.Range(2.0f, 3.0f), Random.Range(2.0f, 3.0f));
            m_speed = new Vector2(Random.Range(m_minSpeed.x, m_maxSpeed.x), Random.Range(m_minSpeed.y, m_maxSpeed.y));
        }

        private void Update()
        {
            m_tiling += m_speed * Time.deltaTime;
            if (m_tiling.x < 2.0f) { m_tiling.x += 1.0f; }
            if (m_tiling.x > 3.0f) { m_tiling.x -= 2.0f; }
            if (m_tiling.y < 2.0f) { m_tiling.y += 1.0f; }
            if (m_tiling.y > 3.0f) { m_tiling.y -= 2.0f; }
            
            m_spriteRenderer.size = m_spriteSize * m_tiling * 2;
        }
    }
}
