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

        private float m_timer;

        private Vector3[] m_positions;
        private int m_currentPositionIndex;

        protected override void Awake()
        {
            m_positions = new[] { m_aPosition, m_bPosition };
            if(!m_offset) { return; }

            for (var i = 0; i < m_positions.Length; i++)
            {
                m_positions[i] += transform.localPosition;
            }
            
            TargetPosition = m_positions[0];
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
            m_timer += Time.deltaTime;
            if(m_timer < m_alternateTime) { return; }
            m_timer -= m_alternateTime;
            
            SwapTarget();
        }

        private void SwapTarget()
        {
            m_currentPositionIndex = 1 - m_currentPositionIndex;
            TargetPosition = m_positions[m_currentPositionIndex];
        }
    }
}
