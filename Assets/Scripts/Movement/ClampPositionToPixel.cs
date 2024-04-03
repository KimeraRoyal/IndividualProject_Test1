using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    public class ClampPositionToPixel : MonoBehaviour
    {
        private Mover[] m_movers;

        [SerializeField] private int m_pixelsPerInch = 100;

        private Vector3 m_lastPosition;

        private void Awake()
        {
            m_movers = GetComponents<Mover>();
        }

        private void Start()
        {
            if (m_movers == null || m_movers.Length < 1) { return; }

            foreach (var mover in m_movers)
            {
                mover.OnMove += Clamp;
            }
            
            Clamp();
        }

        private void Update()
        {
            Clamp();
        }

        private void LateUpdate()
        {
            Clamp();
        }

        private void FixedUpdate()
        {
            Clamp();
        }

        private Vector3 Clamp(Vector3 _position)
        {
            for (var i = 0; i < 2; i++)
            {
                _position = ClampAxis(i, _position);
            }

            return _position;
        }

        private void Clamp()
        {
            var position = transform.localPosition;
            if((position - m_lastPosition).magnitude < 0.001f) { return; }
            
            position = Clamp(position);
            transform.localPosition = position;
        }

        private Vector3 ClampAxis(int _axis, Vector3 _position)
        {
            var factor = 1.0f / m_pixelsPerInch;
            _position[_axis] = Mathf.Round(_position[_axis] / factor) * factor;

            return _position;
        }
    }
}
