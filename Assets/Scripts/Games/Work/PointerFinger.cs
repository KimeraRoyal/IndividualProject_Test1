using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    public class PointerFinger : MonoBehaviour
    {
        [SerializeField] private Camera m_camera;
        
        private Mover m_mover;

        private SpriteAnimationSet[] m_animationSets;
        
        [SerializeField] private LayerMask m_interactableLayerMask;
        [SerializeField] private Transform m_interactionPoint;
        [SerializeField] private float m_interactionCastRadius = 1.0f;

        private bool m_fingerDown;
        private FaceButton m_pressedButton;

        private void Awake()
        {
            m_camera ??= FindObjectOfType<Camera>();
            
            m_mover = GetComponent<Mover>();
            m_animationSets = GetComponentsInChildren<SpriteAnimationSet>();
        }

        private void Update()
        {
            var mouseDown = Input.GetMouseButton(0);
            if(m_fingerDown == mouseDown) { return; }

            m_fingerDown = mouseDown;
            if(m_fingerDown) { Press(); }
            else { Release(); }
        }

        private void Press()
        {
            foreach (var animationSet in m_animationSets)
            {
                animationSet.CurrentAnimationIndex = 1;
            }
            
            var direction = m_camera.transform.forward;
            if(!m_camera.orthographic) { direction = (m_interactionPoint.position - m_camera.transform.position).normalized; }
            
            Debug.DrawRay(m_interactionPoint.position, direction, Color.red, 1.0f);
            if (!Physics.SphereCast(m_interactionPoint.position, m_interactionCastRadius, direction, out var rayHit)) { return; }
            
            m_pressedButton = rayHit.collider.GetComponentInParent<FaceButton>();
            if (!m_pressedButton) { return; }
            
            m_pressedButton.Pressed = true;
            m_mover.Enabled = false;
        }

        private void Release()
        {
            foreach (var animationSet in m_animationSets)
            {
                animationSet.CurrentAnimationIndex = 0;
            }
            
            if (m_pressedButton)
            {
                m_pressedButton.Pressed = false;
                m_mover.Enabled = true;
            }
            m_pressedButton = null;
        }
    }
}
