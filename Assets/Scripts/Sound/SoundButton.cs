using FMODUnity;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(FaceButton))]
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private EventReference m_soundEvent;
        
        private void Awake()
        {
            GetComponent<FaceButton>().OnPressedChange += OnPressedChange;
        }

        private void OnPressedChange(bool _pressed)
        {
            if(!_pressed) { return; }
            RuntimeManager.PlayOneShot(m_soundEvent);
        }
    }
}
