using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    public class PaperGoal : MonoBehaviour
    {
        private Microgame m_microgame;

        private StampArm m_stampArm;

        [SerializeField] private int m_minTargetPapers = 10;
        [SerializeField] private int m_maxTargetPapers = 15;

        private bool m_targetMet;
        private int m_targetPapers;
        private int m_papers;
        
        private void Awake()
        {
            m_microgame = GetComponentInParent<Microgame>();

            m_stampArm = FindObjectOfType<StampArm>();
        }

        private void Start()
        {
            m_targetPapers = Random.Range(m_minTargetPapers, m_maxTargetPapers);
            
            m_stampArm.OnPaperStamped += OnPaperStamped;
        }

        private void OnPaperStamped()
        {
            m_papers++;
            if(m_targetMet || m_papers < m_targetPapers) { return; }
            
            m_microgame.Clear();
            m_targetMet = true;
        }
    }
}
