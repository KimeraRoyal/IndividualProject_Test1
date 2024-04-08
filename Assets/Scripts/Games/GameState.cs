using System;
using System.Linq;
using UnityEngine;

namespace IP1
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] private int m_prescriptionAmount = 3;

        [SerializeField] private bool[] m_pillsPopped = new bool[7 * 4];
        [SerializeField] private int m_sheetsPerBox = 3;
        [SerializeField] private int m_sheetsRemaining = 3;

        [SerializeField] private int m_baseHungriness = 2;
        [SerializeField] private int m_hungriness = 2;

        public int PrescriptionAmount => m_prescriptionAmount;

        public bool[] PillsPopped => m_pillsPopped;

        public int SheetsRemaining => m_sheetsRemaining;
        public int PillsRemaining => m_pillsPopped.Count(_pillPopped => !_pillPopped);

        public bool NeedsRefill => m_sheetsRemaining < 1 && PillsRemaining < m_prescriptionAmount;

        public int Hungriness => m_hungriness;
        public bool Hungry => m_hungriness > 0;

        public void PopPill(int _index)
        {
            if (m_pillsPopped[_index])
            {
                return;
            }

            m_pillsPopped[_index] = true;
        }

        public void PopPill(int _row, int _column)
            => PopPill(_row * 7 + _column);

        public void Refill()
        {
            m_sheetsRemaining += m_sheetsPerBox;
            if(PillsRemaining < 1) { TakeNewSheet(); }
        }

        public bool TakeNewSheet()
        {
            if (m_sheetsRemaining < 1)
            {
                return false;
            }

            for (var i = 0; i < 7 * 4; i++)
            {
                m_pillsPopped[i] = false;
            }
            
            m_sheetsRemaining--;

            return true;
        }

        public void Eat()
            => m_hungriness--;

        public void BecomeHungry()
            => m_hungriness = m_baseHungriness;
    }
}
