using UnityEngine;

namespace IP1
{
    public class Handle : MonoBehaviour
    {
        public void SetAngle(float _angle)
        {
            var euler = transform.localEulerAngles;
            euler.x = _angle;
            transform.localEulerAngles = euler;
        }
    }
}
