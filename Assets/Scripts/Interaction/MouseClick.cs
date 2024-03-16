using System;
using UnityEngine;

namespace IP1.Interaction
{
    public class MouseClick : MonoBehaviour
    {
        private Camera m_camera;

        [SerializeField] private bool m_enabled = true;
        
        [SerializeField] private LayerMask m_interactableLayerMask;

        [SerializeField] private bool m_shootRays = true;
        [SerializeField] private bool m_trackHeld;

        private bool m_clicking;

        public Action<bool> OnClickingChanged;
        
        public Action<RaycastHit> OnInteractableClicked;
        public Action<RaycastHit> OnInteractableHeld;

        public bool Enabled
        {
            get => m_enabled;
            set
            {
                m_enabled = value;
                
                if (m_enabled || !m_clicking) { return; }
                
                m_clicking = false;
                OnClickingChanged?.Invoke(m_clicking);
            }
        }

        private void Awake()
        {
            m_camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            OnClickingChanged += Click;
        }

        private void Update()
        {
            if(!m_enabled) { return; }
            
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
            if (!m_clicking || !m_trackHeld) { return; }

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
            if (!m_shootRays)
            {
                o_rayHit = new RaycastHit();
                return false;
            }
            
            var mousePosition = Input.mousePosition;
            mousePosition.z = m_camera.farClipPlane;
            
            var cameraPosition = m_camera.transform.position;
            var mouseWorldPosition = m_camera.ScreenToWorldPoint(mousePosition);
            var localMouseRay = mouseWorldPosition - cameraPosition;

            var ray = new Ray(cameraPosition, localMouseRay.normalized);

            if (Physics.Raycast(ray, out o_rayHit, localMouseRay.magnitude, m_interactableLayerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * o_rayHit.distance, Color.green, 0.01f);
                return true;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * localMouseRay.magnitude, Color.red, 0.01f);
                return false;
            }
        }
    }
}
