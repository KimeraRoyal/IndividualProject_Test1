using System;
using System.Collections.Generic;
using UnityEngine;

namespace IP1
{
    public class PaperStack : MonoBehaviour
    {
        [SerializeField] private Vector3 m_paperOffset;
        
        private readonly List<Paper> m_papers = new List<Paper>();

        public Action<Paper> OnPaperAdded;

        public Vector3 PaperOffset => m_paperOffset;
        public Vector3 CurrentPaperOffset => m_paperOffset * m_papers.Count;

        public IReadOnlyList<Paper> Papers => m_papers;

        public void AddPaper(Paper _paper)
        {
            _paper.transform.SetParent(transform);
            _paper.Dropped();
            
            m_papers.Add(_paper);
            OnPaperAdded?.Invoke(_paper);
        }
    }
}
