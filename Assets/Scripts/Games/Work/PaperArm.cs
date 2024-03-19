using System;
using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class PaperArm : MonoBehaviour
    {
        private PaperStack m_paperStack;
        
        [SerializeField] private Paper m_paperPrefab;
        [SerializeField] private Vector3 m_paperOffset;

        [SerializeField] private float m_handInTime = 1.0f;
        [SerializeField] private Ease m_handInEase = Ease.Linear;

        [SerializeField] private float m_dropPaperWaitTime = 1.0f;

        [SerializeField] private float m_handOutTime = 1.0f;
        [SerializeField] private Ease m_handOutEase = Ease.Linear;

        private Vector3 m_startingPosition;

        private Paper m_heldPaper;

        private Sequence m_sequence;

        public Action<Paper> OnPaperCreated;
        public Action<Paper> OnPaperDropped;

        private void Awake()
        {
            m_paperStack = FindObjectOfType<PaperStack>();
            
            m_startingPosition = transform.position;
        }

        private void Start()
        {
            OnPaperCreated += _paper => { _paper.transform.position += m_paperStack.CurrentPaperOffset; };
            OnPaperDropped += m_paperStack.AddPaper;
            
            SpawnPaper();
        }

        private void Update()
        {
            if(m_heldPaper) { return; }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                SpawnPaper();
            }
        }

        private void SpawnPaper()
        {
            if (m_sequence is { active: true }) { m_sequence.Kill(); }
            
            var localTransform = transform;
            
            m_heldPaper = Instantiate(m_paperPrefab, localTransform.position + m_paperOffset, Quaternion.identity, localTransform);
            OnPaperCreated?.Invoke(m_heldPaper);

            m_sequence = DOTween.Sequence();
            m_sequence.Append(localTransform.DOMove(Vector3.zero, m_handInTime).SetEase(m_handInEase));
            m_sequence.AppendCallback(DropPaper);
            m_sequence.AppendInterval(m_dropPaperWaitTime);
            m_sequence.Append(localTransform.DOMove(m_startingPosition, m_handOutTime).SetEase(m_handOutEase));
        }

        private void DropPaper()
        {
            m_heldPaper.transform.SetParent(null);
            OnPaperDropped?.Invoke(m_heldPaper);
            
            m_heldPaper = null;
        }
    }
}
