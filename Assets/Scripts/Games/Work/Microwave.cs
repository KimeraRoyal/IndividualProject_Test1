using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    public class Microwave : MonoBehaviour
    {
        private GameState m_state;
        private Microgame m_microgame;
        
        private Timer m_timer;
        
        private Digits m_digits;
        private RotateInPlace m_rotator;
        private ColourChanger m_colourChanger;

        [SerializeField] private int m_minTimerValue = 60;
        [SerializeField] private int m_maxTimerValue = 120;

        [SerializeField] private Transform m_meal;

        [SerializeField] private FaceButton[] m_incidentalButtons;
        [SerializeField] private FaceButton m_startButton;

        [SerializeField] private GameObject m_lights;

        private bool m_on;

        public Action OnActivated;
        
        private void Awake()
        {
            m_state = GetComponentInParent<GameState>();
            m_microgame = GetComponentInParent<Microgame>();
            
            m_timer = GetComponent<Timer>();
            
            m_digits = GetComponentInChildren<Digits>();
            m_rotator = GetComponentInChildren<RotateInPlace>();
            m_colourChanger = GetComponentInChildren<ColourChanger>();

            Instantiate(m_meal, m_rotator.transform);
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

            OnActivated += m_microgame.Clear;
            if (m_state) { OnActivated += m_state.Eat; }
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

            OnActivated?.Invoke();
        }
    }
}
