using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    public class AlternatePosition : Mover
    {
        [SerializeField] private Vector3 m_aPosition;
        [SerializeField] private Vector3 m_bPosition;
        [SerializeField] private float m_alternateTime = 1.0f;
        [SerializeField] private bool m_offset;

        [SerializeField] private bool m_alternateOn = true;

        private float m_timer;

        private Vector3[] m_positions;
        private int m_currentPositionIndex;

        private Vector3 m_startingPosition;

        public bool AlternateOn
        {
            get => m_alternateOn;
            set => m_alternateOn = value;
        }
        
        public Vector3 StartingPosition
        {
            get => m_startingPosition;
            set => m_startingPosition = value;
        }

        protected override void Awake()
        {
            m_positions = new[] { m_aPosition, m_bPosition };
            m_startingPosition = transform.localPosition;

            var newTargetPosition = m_positions[m_currentPositionIndex];
            if (m_offset) { newTargetPosition += m_startingPosition; }
            TargetPosition = newTargetPosition;
        }

        protected override void Update()
        {
            RunTimer();
            base.Update();
        }

        protected override void Start()
        {
            base.Start();

            SwapTarget();
        }

        private void RunTimer()
        {
            if(!m_alternateOn) { return; }
            
            m_timer += Time.deltaTime;
            if(m_timer < m_alternateTime) { return; }
            m_timer -= m_alternateTime;
            
            SwapTarget();
        }

        private void SwapTarget()
        {
            m_currentPositionIndex = 1 - m_currentPositionIndex;
            
            var newTargetPosition = m_positions[m_currentPositionIndex];
            if (m_offset) { newTargetPosition += m_startingPosition; }
            TargetPosition = newTargetPosition;
        }
    }
}
