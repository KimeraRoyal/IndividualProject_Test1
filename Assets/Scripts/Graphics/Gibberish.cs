using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    public class Gibberish : MonoBehaviour
    {
        private TMP_Text m_text;
        
        [SerializeField] private int m_maxLineLength = 1;
        [SerializeField] private int m_lines = 1;

        [SerializeField] private int m_minWordLength = 1;
        [SerializeField] private int m_maxWordLength = 1;

        [SerializeField] private int m_minSpaceLength = 1;
        [SerializeField] private int m_maxSpaceLength = 1;

        [SerializeField] private bool m_randomization;
        [SerializeField] private float m_randomizationTime = 1.0f;

        private float m_timer;
        
        private void Awake()
        {
            m_text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            Generate();
        }

        private void Update()
        {
            if(!m_randomization) { return; }
            
            m_timer += Time.deltaTime;
            if(m_timer < m_randomizationTime) { return; }
            m_timer -= m_randomizationTime;

            Generate();
        }

        public void Generate()
        {
            var text = "";
            
            for (var i = 0; i < m_lines; i++)
            {
                var spaceOrText = Random.Range(0, 2);
                var isSpace = spaceOrText != 0;
                
                var remainingCharacters = m_maxLineLength;
                while (remainingCharacters > m_minSpaceLength && remainingCharacters > m_minWordLength)
                {
                    var minLength = isSpace ? m_minSpaceLength : m_minWordLength;
                    var maxLength = isSpace ? m_maxSpaceLength : m_maxWordLength;
                    var length = Random.Range(minLength, maxLength + 1);

                    text += GenerateWord(length, isSpace ? _ => ' ' : GenerateLetter);

                    remainingCharacters -= length;
                    isSpace = !isSpace;
                }
                text += "\n";
            }

            m_text.text = text;
        }

        private string GenerateWord(int _length, Func<bool, char> _generateLetter)
        {
            var word = "";
            for (var j = 0; j < _length; j++)
            {
                word += _generateLetter(j == 0);
            }
            return word;
        }

        private char GenerateLetter(bool _isCaps)
        {
            var min = _isCaps ? 'A' : 'a';
            var max = _isCaps ? 'Z' : 'z';
            return (char) Random.Range(min, max + 1);
        }
    }
}
