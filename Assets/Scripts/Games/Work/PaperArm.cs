using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class PaperArm : MonoBehaviour
    {
        private GameState m_state;
        
        private PaperStack m_paperStack;
        private StampArm m_stampArm;
        
        [SerializeField] private Paper m_paperPrefab;
        [SerializeField] private Paper m_initialStackPaperPrefab;
        [SerializeField] private Vector3 m_paperOffset;

        [SerializeField] private float m_newPaperWaitTime = 1.0f;

        [SerializeField] private float m_handInTime = 1.0f;
        [SerializeField] private Ease m_handInEase = Ease.Linear;

        [SerializeField] private float m_dropPaperWaitTime = 1.0f;

        [SerializeField] private float m_handOutTime = 1.0f;
        [SerializeField] private Ease m_handOutEase = Ease.Linear;

        private Vector3 m_startingPosition;

        private Paper m_heldPaper;
        private bool m_spawning;

        private Sequence m_sequence;

        public Action<Paper> OnPaperCreated;
        public Action<Paper> OnPaperDropped;

        private void Awake()
        {
            m_state = GetComponentInParent<GameState>();
            
            m_paperStack = FindObjectOfType<PaperStack>();
            m_stampArm = FindObjectOfType<StampArm>();
            
            OnPaperDropped += m_paperStack.AddPaper;

            m_paperStack.OnPaperAdded += OnPaperAdded;
            m_stampArm.OnPaperStamped += OnPaperStamped;
            
            m_startingPosition = transform.position;
        }

        private void Start()
        {
            SpawnInitialStack();
            SpawnPaper();
        }

        private void Update()
        {
            if(m_heldPaper) { return; }
        }

        private void OnPaperAdded(Paper _paper)
        {
            m_startingPosition += m_paperStack.PaperOffset;
        }

        private void OnPaperStamped()
        {
            StartCoroutine(WaitForNewPaper());
        }

        private IEnumerator WaitForNewPaper()
        {
            yield return new WaitUntil(() => !m_spawning);
            yield return new WaitForSeconds(m_newPaperWaitTime);
            SpawnPaper();
        }

        private void SpawnInitialStack()
        {
            if(!m_state) { return; }

            for (var i = 0; i < m_state.PaperStackSize; i++)
            {
                var paper = Instantiate(m_initialStackPaperPrefab, m_paperOffset + m_paperStack.CurrentPaperOffset, Quaternion.identity, transform);
                m_paperStack.AddPaper(paper);
            }
        }

        private void SpawnPaper()
        {
            if (m_sequence is { active: true }) { m_sequence.Kill(); }
            
            var localTransform = transform;
            
            m_spawning = true;
            m_heldPaper = Instantiate(m_paperPrefab, localTransform.position + m_paperOffset, Quaternion.identity, localTransform);
            OnPaperCreated?.Invoke(m_heldPaper);

            m_sequence = DOTween.Sequence();
            m_sequence.Append(localTransform.DOMove(m_paperStack.CurrentPaperOffset, m_handInTime).SetEase(m_handInEase));
            m_sequence.AppendCallback(DropPaper);
            m_sequence.AppendInterval(m_dropPaperWaitTime);
            m_sequence.Append(localTransform.DOMove(m_startingPosition, m_handOutTime).SetEase(m_handOutEase));
            m_sequence.AppendCallback(FinishSpawning);
        }

        private void FinishSpawning()
        {
            m_spawning = false;
        }

        private void DropPaper()
        {
            m_heldPaper.transform.SetParent(null);
            OnPaperDropped?.Invoke(m_heldPaper);
            m_state.PaperStackSize++;
            
            m_heldPaper = null;
        }
    }
}
