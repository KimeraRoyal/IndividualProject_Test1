using UnityEngine;

namespace IP1
{
    public class Handle : MonoBehaviour
    {
        [SerializeField] private float m_minAngle = -180;
        [SerializeField] private float m_maxAngle = 180;
        
        // TODO: With how this class and TurnHandle work, the angle will snap if you hold the handle up and then trace a circle clockwise. This implies a different approach is needed.
        public void SetAngle(float _angle)
        {
            if (_angle > 180) { _angle = _angle - 360; }
            _angle = Mathf.Clamp(_angle, m_minAngle, m_maxAngle);
            
            transform.localEulerAngles = Vector3.forward * _angle;
        }
    }
}
