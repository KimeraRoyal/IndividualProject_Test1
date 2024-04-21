using DG.Tweening;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace IP1
{
    public class Door : MonoBehaviour
    {
        private Handle m_handle;

        [SerializeField] private Vector3 m_openRotation;
        [SerializeField] private float m_openTime = 1.0f;
        [SerializeField] private Ease m_openEasing = Ease.Linear;

        [SerializeField] private EventReference m_openEvent;
        [SerializeField] private EventReference m_ambienceEvent;
        [ParamRef] [SerializeField] private string m_ambienceMuffleEvent;

        private EventDescription m_ambienceDescription;
        private EventInstance m_ambienceInstance;
        
        private void Awake()
        {
            m_handle = GetComponentInChildren<Handle>();
        }

        private void Start()
        {
            m_ambienceDescription = RuntimeManager.GetEventDescription(m_ambienceEvent);
            m_ambienceDescription.createInstance(out m_ambienceInstance);
            m_ambienceInstance.start();
            
            m_handle.OnOpened += OnOpened;
        }

        private void OnDestroy()
        {
            m_ambienceInstance.stop(STOP_MODE.ALLOWFADEOUT);
            m_ambienceInstance.release();
        }

        private void OnOpened()
        {
            transform.DOLocalRotate(m_openRotation, m_openTime).SetEase(m_openEasing);
            
            RuntimeManager.PlayOneShot(m_openEvent);
            m_ambienceInstance.setParameterByName(m_ambienceMuffleEvent, 0.0f);
        }
    }
}
