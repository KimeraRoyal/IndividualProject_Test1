using UnityEngine;

namespace IP1
{
    public class RotateInPlace : MonoBehaviour
    {
        [SerializeField] private Vector3 m_rotationSpeed;

        private void Update()
        {
            transform.localEulerAngles += m_rotationSpeed * Time.deltaTime;
        }
    }
}
