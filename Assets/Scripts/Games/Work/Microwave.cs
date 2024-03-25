using DG.Tweening;
using UnityEngine;

namespace IP1
{
    public class Microwave : MonoBehaviour
    {
        private Timer m_timer;
        
        private Digits m_digits;
        private RotateInPlace m_rotator;
        private ColourChanger m_colourChanger;

        [SerializeField] private int m_minTimerValue = 60;
        [SerializeField] private int m_maxTimerValue = 120;

        [SerializeField] private FaceButton[] m_incidentalButtons;
        [SerializeField] private FaceButton m_startButton;

        [SerializeField] private GameObject m_lights;

        private bool m_on;
        
        private void Awake()
        {
            m_timer = GetComponent<Timer>();
            
            m_digits = GetComponentInChildren<Digits>();
            m_rotator = GetComponentInChildren<RotateInPlace>();
            m_colourChanger = GetComponentInChildren<ColourChanger>();
        }

        private void Start()
        {
            m_timer.enabled = false;
            m_rotator.enabled = false;
            m_lights.SetActive(false);

            m_timer.OnTick += OnTick;

            foreach (var button in m_incidentalButtons)
            {
                button.OnPressedChange += OnIncidentalPressedChange;
            }
            m_startButton.OnPressedChange += OnPressedChange;
        }

        private void OnTick()
        {
            m_digits.Ticks--;
        }

        private void OnIncidentalPressedChange(bool _pressed)
        {
            if(!_pressed) { return; }
            
            m_digits.Ticks = Random.Range(m_minTimerValue, m_maxTimerValue);
        }

        private void OnPressedChange(bool _pressed)
        {
            if(!_pressed || m_on) { return; }
            
            m_on = true;

            m_lights.SetActive(true);

            var sequence = DOTween.Sequence();
            sequence.Append(m_colourChanger.ChangeColor(1));
            sequence.AppendCallback(() =>
            {
                m_timer.enabled = true;
                m_rotator.enabled = true;
            });
        }
    }
}
