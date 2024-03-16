using UnityEngine;

namespace IP1
{
    public class CopyRotation : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        [SerializeField] private float m_multiply = 1.0f;

        private void Update()
        {
            transform.localEulerAngles = Vector3.forward * (m_target.localEulerAngles.z * m_multiply);
        }
    }
}
