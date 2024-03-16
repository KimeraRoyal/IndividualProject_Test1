using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class MouseMovement : MonoBehaviour
    {
        public Action<Vector2> OnMouseMoved;

        private void Update()
        {
            var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (mouseDelta.magnitude > 0.001f)
            {
                OnMouseMoved?.Invoke(mouseDelta);
            }
        }
    }
}
