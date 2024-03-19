using UnityEngine;

namespace IP1
{
    public class TransformDistanceScale : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        [SerializeField] private float m_minDistance = 0.0f, m_maxDistance = 1.0f;
        [SerializeField] private float m_distanceOffset = 0.0f;
        
        [SerializeField] private Vector3 m_a = Vector3.zero, m_b = Vector3.one;

        private void Update()
        {
            var difference = Vector3.Distance(transform.position, m_target.position) + m_distanceOffset;
            difference = Mathf.Clamp(difference, m_minDistance, m_maxDistance);

            var t = (difference - m_minDistance) / (m_maxDistance - m_minDistance);

            transform.localScale = Vector3.Lerp(m_a, m_b, t);
        }
    }
}
