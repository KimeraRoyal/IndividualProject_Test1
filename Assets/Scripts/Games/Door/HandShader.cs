using System;
using DG.Tweening;
using IP1.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace IP1.Games.Door
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HandShader : MonoBehaviour
    {
        private MouseClick m_mouseClick;

        private SpriteRenderer m_sprite;

        [SerializeField] private Color m_defaultColor;
        [SerializeField] private Color m_heldColor;

        [SerializeField] private float m_colorFadeDuration = 1.0f;
        
        private Tween m_colorChangeTween;

        private bool m_holding;

        private void Awake()
        {
            m_mouseClick = GetComponentInParent<MouseClick>();

            m_sprite = GetComponent<SpriteRenderer>();
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
            ChangeColor(m_defaultColor);
        }

        private void OnInteractableClicked(RaycastHit _rayHit)
        {
            m_holding = true;
            ChangeColor(m_heldColor);
        }

        private void ChangeColor(Color _color)
        {
            if (m_colorChangeTween is { active: true }) { m_colorChangeTween.Kill(); }

            m_colorChangeTween = m_sprite.DOColor(_color, m_colorFadeDuration);
        }
    }
}
