using UnityEngine;

namespace IP1
{
    public class Paper : MonoBehaviour
    {
        [SerializeField] private GameObject m_stampMarkingPrefab;
        [SerializeField] private Vector3 m_stampMarkingOffset;
        
        public void CreateStampMarking(Vector3 _stampPosition)
        {
            var position = _stampPosition;
            position.z = transform.position.z;
            position += m_stampMarkingOffset;
            
            Instantiate(m_stampMarkingPrefab, position, Quaternion.identity, transform);
        }
    }
}
