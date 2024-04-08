using UnityEngine;

namespace IP1
{
    public class MoveWithPaperStack : MonoBehaviour
    {
        private PaperStack m_paperStack;

        private void Awake()
        {
            m_paperStack = FindObjectOfType<PaperStack>();
                
            m_paperStack.OnPaperAdded += OnPaperAdded;
        }

        private void OnPaperAdded(Paper _paper)
        {
            transform.position += m_paperStack.PaperOffset;
        }
    }
}
