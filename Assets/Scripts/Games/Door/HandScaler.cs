using DG.Tweening;
using IP1.Interaction;
using UnityEngine;

namespace IP1.Games.Door
{
    public class HandScaler : MonoBehaviour
    {
        private MouseClick m_mouseClick;

        [SerializeField] private float m_defaultScale;
        [SerializeField] private float m_heldScale;

        [SerializeField] private float m_scaleDuration = 1.0f;
        
        private Tween m_scaleTween;

        private bool m_holding;

        private void Awake()
        {
            m_mouseClick = GetComponentInParent<MouseClick>();
        }

        private void Start()
        {
            m_mouseClick.OnClickingChanged += OnClickingChanged;
            m_mouseClick.OnInteractableClicked += OnInteractableClicked;
        }

        private void OnClickingChanged(bool _clicked)
        {
            if (!m_holding || _clicked) { return; }

            m_holding = false;
            ChangeScale(m_defaultScale);
        }

        private void OnInteractableClicked(RaycastHit _rayHit)
        {
            m_holding = true;
            ChangeScale(m_heldScale);
        }

        private void ChangeScale(float _scale)
        {
            if (m_scaleTween is { active: true }) { m_scaleTween.Kill(); }

            m_scaleTween = transform.DOScale(Vector3.one * _scale, m_scaleDuration);
        }
    }
}
