using UnityEngine;

namespace IP1
{
    public class ClampRotation : MonoBehaviour
    {
        private Rotator[] m_rotators;

        [SerializeField] private bool m_xClampToBounds;
        [SerializeField] private bool m_yClampToBounds;
        
        [SerializeField] private Vector2 m_minRotation, m_maxRotation;

        [SerializeField] private bool m_useLocalRotation;

        public Vector2 MinRotation { get => m_minRotation; set => m_minRotation = value; }
        public Vector2 MaxRotation { get => m_maxRotation; set => m_maxRotation = value; }
        
        private void Awake()
        {
            m_rotators = GetComponents<Rotator>();
        }

        private void Start()
        {
            if (m_rotators == null || m_rotators.Length < 1) { return; }

            foreach (var rotator in m_rotators)
            {
                rotator.OnRotateTarget += Clamp;
                rotator.OnRotate += Clamp;
            }
            
            Clamp();
        }

        private void FixedUpdate()
        {
            Clamp();
        }

        private Vector3 Clamp(Vector3 _rotation)
        {
            if (m_xClampToBounds) { _rotation = ClampAxis(0, _rotation); }
            if (m_yClampToBounds) { _rotation = ClampAxis(1, _rotation); }

            return _rotation;
        }

        private void Clamp()
        {
            var rotation = m_useLocalRotation ? transform.localEulerAngles : transform.eulerAngles;

            rotation = Clamp(rotation);

            if (m_useLocalRotation)
            {
                transform.localEulerAngles = rotation;
            }
            else
            {
                transform.eulerAngles = rotation;
            }
        }

        private Vector3 ClampAxis(int _axis, Vector3 _rotation)
        {
            _rotation[_axis] = Mathf.Clamp(_rotation[_axis], m_minRotation[_axis], m_maxRotation[_axis]);
            return _rotation;
        }
    }
}
