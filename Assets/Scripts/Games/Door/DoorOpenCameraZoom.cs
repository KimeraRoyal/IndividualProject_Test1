using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class DoorOpenCameraZoom : MonoBehaviour
    {
        private Handle m_handle;

        [SerializeField] private Vector3 m_openPosition;
        [SerializeField] private float m_openTime = 1.0f;
        [SerializeField] private Ease m_openEasing = Ease.Linear;

        private void Awake()
        {
            m_handle = FindObjectOfType<Handle>();
        }

        private void Start()
        {
            m_handle.OnOpened += OnOpened;
        }

        private void OnOpened()
        {
            transform.DOLocalMove(m_openPosition, m_openTime).SetEase(m_openEasing);
        }
    }
}
