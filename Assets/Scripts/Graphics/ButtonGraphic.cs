using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ButtonGraphic : MonoBehaviour
    {
        private FaceButton m_button;
        
        private SpriteRenderer m_spriteRenderer;

        [SerializeField] private Sprite m_pressedSprite, m_unpressedSprite;

        private void Awake()
        {
            m_button = GetComponentInParent<FaceButton>();

            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            m_button.OnPressedChange += OnPressedChange;
        }

        private void OnPressedChange(bool _pressed)
        {
            m_spriteRenderer.sprite = _pressed ? m_pressedSprite : m_unpressedSprite;
        }
    }
}
