using UnityEngine;

namespace IP1
{
    public class StateSaver : MonoBehaviour
    {
        [SerializeField] private int m_prescriptionAmount = 3;
        
        [SerializeField] private bool[] m_pillsPopped = new bool[7 * 4];
        [SerializeField] private int m_sheetsRemaining = 3;

        private int m_pillsRemaining;

        public bool[] PillsPopped => m_pillsPopped;

        public int SheetsRemaining => m_sheetsRemaining;

        public bool NeedsRefill => m_sheetsRemaining < 1 && m_pillsRemaining < m_prescriptionAmount; 

        private void Awake()
        {
            m_pillsRemaining = 7 * 4;
        }

        public void PopPill(int _index)
        {
            if(m_pillsPopped[_index]) { return; }
            
            m_pillsPopped[_index] = true;
        }

        public void PopPill(int _row, int _column)
            => PopPill(_row * 7 + _column);

        public void Refill()
        {
            m_sheetsRemaining += 3;
        }

        public bool TakeNewSheet()
        {
            if (m_sheetsRemaining < 1) { return false; }
            
            m_sheetsRemaining--;
            for(var i = 0; i < 7 * 4; i++)
            {
                m_pillsPopped[i] = false;
            }

            return true;
        }
    }
}
