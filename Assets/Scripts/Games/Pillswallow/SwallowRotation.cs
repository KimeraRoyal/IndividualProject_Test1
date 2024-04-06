using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class SwallowRotation : MonoBehaviour
    {
        private OpenPalm m_palm;
        
        [SerializeField] private float m_upRotation = 1.0f;
        [SerializeField] private float m_upTime = 1.0f;
        [SerializeField] private Ease m_upEase = Ease.Linear;

        [SerializeField] private float m_waitTime = 1.0f;

        [SerializeField] private float m_downTime = 1.0f;
        [SerializeField] private Ease m_downEase = Ease.Linear;

        private void Awake()
        {
            m_palm = FindObjectOfType<OpenPalm>();
        }

        private void Start()
        {
            m_palm.OnSwallow += OnSwallow;
        }

        private void OnSwallow()
        {
            var swallowSequence = DOTween.Sequence();
            swallowSequence.Append(transform.DOLocalRotate(Vector3.right * m_upRotation, m_upTime).SetEase(m_upEase));
            swallowSequence.AppendInterval(m_waitTime);
            swallowSequence.Append(transform.DOLocalRotate(Vector3.zero, m_downTime).SetEase(m_downEase));
        }
    }
}
