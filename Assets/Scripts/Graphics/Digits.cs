using System;
using System.Collections.Generic;
using UnityEngine;

namespace IP1
{
    public class Digits : MonoBehaviour
    {
        [SerializeField] private Digit[] m_digits;

        [SerializeField] private int[] m_divisors;
        [SerializeField] private int m_ticks;

        private int m_lastTicks = -1;

        public int Ticks
        {
            get => m_ticks;
            set => m_ticks = value;
        }

        private void Awake()
        {
            m_digits = GetComponentsInChildren<Digit>();
        }

        private void Update()
        {
            if(m_lastTicks == m_ticks) { return; }
            
            var digits = new int[m_digits.Length];
            CalculateDigits(digits);

            for (var i = 0; i < m_digits.Length; i++)
            {
                m_digits[i].CurrentDigit = digits[i];
            }

            m_lastTicks = m_ticks;
        }

        private void CalculateDigits(IList<int> _digits)
        {
            var carryOver = m_ticks;
            for (var i = _digits.Count - 1; i >= 0; i--)
            {
                var divisor = m_divisors[i % m_divisors.Length];
                _digits[i] = carryOver % divisor;
                carryOver /= divisor;
            }
        }
    }
}
