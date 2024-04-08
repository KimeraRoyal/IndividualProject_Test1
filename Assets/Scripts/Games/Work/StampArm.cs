using System;
using IP1.Movement;
using UnityEngine;

namespace IP1
{
    public class StampArm : MonoBehaviour
    {
        private PaperStack m_paperStack;

        private ClampPosition m_clampPosition;
        
        [SerializeField] private float m_minReloadY;
        [SerializeField] private float m_maxStampY;

        [SerializeField] private float m_minVelocityForStamping = 0.5f;
        [SerializeField] private float m_minVelocityDegradation = 1.0f;
        
        [SerializeField] private LayerMask m_boxcastLayerMask;
        [SerializeField] private Vector2 m_boxcastOrigin;
        [SerializeField] private Vector2 m_boxcastSize = Vector2.one;

        [SerializeField] private Transform m_stampPoint;

        private Vector3 m_lastPosition;
        private float m_velocity;
        
        private float m_minVelocity;
        private float m_minVelocitySmoothingVelocity;

        private bool m_loaded;

        public Action OnPaperStamped;

        private void Awake()
        {
            m_paperStack = FindObjectOfType<PaperStack>();

            m_clampPosition = GetComponent<ClampPosition>();

            m_paperStack.OnPaperAdded += OnPaperAdded;
        }

        private void Start()
        {
            m_loaded = true;
        }

        private void Update()
        {
            Reload();
            CalculateVelocity();
            DetectStamp();
        }

        private void Reload()
        {
            if (m_loaded || transform.localPosition.y < m_minReloadY + m_paperStack.CurrentPaperOffset.y) { return; }

            m_loaded = true;
        }

        private void CalculateVelocity()
        {
            var position = transform.position;
            m_velocity = (position - m_lastPosition).y;
            m_minVelocity = Mathf.Min(m_minVelocity, m_velocity);
            m_minVelocity = Mathf.SmoothDamp(m_minVelocity, 0.0f, ref m_minVelocitySmoothingVelocity, m_minVelocityDegradation);
            m_lastPosition = position;
        }

        private void DetectStamp()
        {
            if(!m_loaded || transform.localPosition.y > m_maxStampY + m_paperStack.CurrentPaperOffset.y) { return; }
            m_loaded = false;

            var rayHit = Physics2D.BoxCast((Vector2) transform.position + m_boxcastOrigin, m_boxcastSize, 0, Vector2.zero, 0, m_boxcastLayerMask);
            if(rayHit.collider == null) { return; }

            if (m_minVelocity < -m_minVelocityForStamping)
            {
                StampPaper(rayHit.collider);
            }
        }
        

        private void StampPaper(Behaviour _paperCollider)
        {
            var paper = _paperCollider.GetComponentInParent<Paper>();
            if (!paper) { return; }
            
            paper.CreateStampMarking(m_stampPoint.position);

            _paperCollider.enabled = false;
            
            OnPaperStamped?.Invoke();
        }

        private void OnPaperAdded(Paper _paper)
        {
            var offset = Vector3.up * m_paperStack.PaperOffset.y;
            m_clampPosition.MinBounds += offset;
            m_clampPosition.MaxBounds += offset;
        }
    }
}
