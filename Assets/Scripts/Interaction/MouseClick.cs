using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class MouseClick : MonoBehaviour
    {
        private Camera m_camera;
        
        [SerializeField] private LayerMask m_interactableLayerMask;

        private bool m_clicking;

        public Transform m_objectToMove;

        public Action<bool> OnClickingChanged;
        
        public Action<RaycastHit> OnInteractableClicked;
        public Action<RaycastHit> OnInteractableHeld;

        private void Awake()
        {
            m_camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            OnClickingChanged += Click;
            OnInteractableClicked += _rayHit => { m_objectToMove.position = _rayHit.point; };
            OnInteractableHeld += _rayHit => { m_objectToMove.position = Vector3.Lerp(m_objectToMove.position, _rayHit.point, 0.1f); };
        }

        private void Update()
        {
            CheckInput();
            HoldInput();
        }

        private void CheckInput()
        {
            var clicking = Input.GetMouseButton(0);
            if (clicking == m_clicking) { return; }
            
            m_clicking = clicking;
            OnClickingChanged?.Invoke(m_clicking);
        }

        private void HoldInput()
        {
            if (!m_clicking) { return; }

            if (!ShootRay(out var rayHit)) { return; }
            
            OnInteractableHeld?.Invoke(rayHit);
        }

        private void Click(bool _click)
        {
            if (!_click) { return; }

            if (!ShootRay(out var rayHit)) { return; }
            
            OnInteractableClicked?.Invoke(rayHit);
        }

        private bool ShootRay(out RaycastHit o_rayHit)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = m_camera.farClipPlane;
            
            var cameraPosition = m_camera.transform.position;
            var mouseWorldPosition = m_camera.ScreenToWorldPoint(mousePosition);
            var localMouseRay = mouseWorldPosition - cameraPosition;

            var ray = new Ray(cameraPosition, localMouseRay.normalized);
            Debug.DrawRay(ray.origin, ray.direction * localMouseRay.magnitude, Color.red, 1);

            return Physics.Raycast(ray, out o_rayHit, localMouseRay.magnitude, m_interactableLayerMask);
        }
    }
}