using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(MouseClick), typeof(MouseFollower))]
    public class DoorHand : MonoBehaviour
    {
        private Handle m_handle;
        
        private MouseClick m_mouseClick;
        private MouseFollower m_mouseFollower;

        private bool m_doorOpened;

        private void Awake()
        {
            m_handle = FindObjectOfType<Handle>();
            
            m_mouseClick = GetComponent<MouseClick>();
            m_mouseFollower = GetComponent<MouseFollower>();
        }

        private void Start()
        {
            m_mouseClick.OnClickingChanged += OnClickingChanged;
            m_mouseClick.OnInteractableClicked += OnInteractableClicked;

            m_handle.OnOpened += () =>
            {
                OnClickingChanged(false);
                m_doorOpened = true;
            };
        }

        private void OnClickingChanged(bool _clicked)
        {
            if (m_mouseFollower.Enabled || _clicked) { return; }
            
            m_mouseFollower.Enabled = true;

            var transformRef = transform;
            transformRef.SetParent(null);
            transformRef.localEulerAngles = Vector3.zero;
        }

        private void OnInteractableClicked(RaycastHit _rayHit)
        {
            if (m_doorOpened || !m_mouseFollower.Enabled) { return; }
            
            m_mouseFollower.Enabled = false;
            transform.SetParent(m_handle.GripPoint);
            m_mouseFollower.TargetPosition = new Vector3(0, 0, transform.localPosition.z);
        }
    }
}
