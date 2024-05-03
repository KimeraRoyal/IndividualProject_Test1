using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Exit : MonoBehaviour
{
    private Image m_fade;

    private TMP_Text m_timerText;

    [SerializeField] private float m_closeTime = 1.0f;
    [SerializeField] private float m_resetSpeed = 1.0f;
    
    [SerializeField] private AnimationCurve m_fadeCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);

    [SerializeField] private string m_timerFormat = "{0}";

    private float m_timer;

    private void Awake()
    {
        m_fade = GetComponent<Image>();

        m_timerText = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        Fade();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Close();
        }
        else
        {
            m_timer = Mathf.Clamp(m_timer - Time.deltaTime * m_resetSpeed, 0.0f, m_closeTime);
        }

        Fade();
        Timer();
    }

    private void Fade()
    {
        var color = m_fade.color;
        color.a = m_fadeCurve.Evaluate(m_timer / m_closeTime);
        m_fade.color = color;
    }

    private void Timer()
    {
        var closing = m_timer > 0.001f;

        m_timerText.enabled = closing;
        if(!closing) { return; }

        m_timerText.text = string.Format(m_timerFormat, Mathf.Clamp(m_closeTime - m_timer, 0.0f, m_closeTime));
        m_timerText.color = m_timer > m_closeTime - 1.0f ? Color.red : Color.white;
    }

    private void Close()
    {
        m_timer += Time.deltaTime;
        if(m_timer < m_closeTime) { return; }
        
        Application.Quit();
    }
}
