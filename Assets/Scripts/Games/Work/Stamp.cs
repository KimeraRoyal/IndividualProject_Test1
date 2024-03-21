using IP1.Movement;
using UnityEngine;

namespace IP1
{
    public class Stamp : MonoBehaviour
    {
        private PaperStack m_paperStack;

        private ClampPosition m_clampPosition;
        
        [SerializeField] private float m_minReloadY;
        [SerializeField] private float m_maxStampY;
        
        [SerializeField] private LayerMask m_boxcastLayerMask;
        [SerializeField] private Vector2 m_boxcastOrigin;
        [SerializeField] private Vector2 m_boxcastSize = Vector2.one;

        [SerializeField] private Transform m_stampPoint;

        private bool m_loaded;

        private void Awake()
        {
            m_paperStack = FindObjectOfType<PaperStack>();

            m_clampPosition = GetComponent<ClampPosition>();
        }

        private void Start()
        {
            m_loaded = true;

            m_paperStack.OnPaperAdded += OnPaperAdded;
        }

        private void Update()
        {
            Reload();
            StampPaper();
        }

        private void Reload()
        {
            if (m_loaded || transform.localPosition.y < m_minReloadY + m_paperStack.CurrentPaperOffset.y) { return; }

            m_loaded = true;
        }

        private void StampPaper()
        {
            if(!m_loaded || transform.localPosition.y > m_maxStampY + m_paperStack.CurrentPaperOffset.y) { return; }

            var rayHit = Physics2D.BoxCast((Vector2) transform.position + m_boxcastOrigin, m_boxcastSize, 0, Vector2.zero, 0, m_boxcastLayerMask);
            if(rayHit.collider == null) { return; }

            var paper = rayHit.collider.GetComponentInParent<Paper>();
            if (!paper) { return; }
            
            paper.CreateStampMarking(m_stampPoint.position);

            rayHit.collider.enabled = false;
            m_loaded = false;
        }

        private void OnPaperAdded(Paper _paper)
        {
            var offset = Vector2.up * m_paperStack.PaperOffset.y;
            m_clampPosition.MinBounds += offset;
            m_clampPosition.MaxBounds += offset;
        }
    }
}
