using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace IP1
{
    [RequireComponent(typeof(SpriteRenderer))] [ExecuteInEditMode]
    public class ScrollSprite : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        [SerializeField] private Vector2 m_spriteSize = Vector2.one;
        [SerializeField] private Vector2 m_speed;

        private Vector2 m_tiling = Vector2.one * 2;

        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
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
