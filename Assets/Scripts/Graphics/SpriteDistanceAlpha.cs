using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteDistanceAlpha : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;

        [SerializeField] private Transform m_target;

        [SerializeField] private float m_minDistance = 0.0f, m_maxDistance = 1.0f;
        [SerializeField] private float m_distanceOffset = 0.0f;
        
        [SerializeField] private float m_a = 0.0f, m_b = 1.0f;

        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var difference = Vector3.Distance(transform.position, m_target.position) + m_distanceOffset;
            difference = Mathf.Clamp(difference, m_minDistance, m_maxDistance);

            var t = (difference - m_minDistance) / (m_maxDistance - m_minDistance);

            var color = m_spriteRenderer.color;
            color.a = Mathf.Lerp(m_a, m_b, t);
            m_spriteRenderer.color = color;
        }
    }
}
