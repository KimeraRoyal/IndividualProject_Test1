using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace IP1
{
    public class ColourChanger : MonoBehaviour
    {
        private SpriteRenderer[] m_sprites;

        [SerializeField] private Color[] m_colors;
        
        [SerializeField] private float m_colorChangeTime = 1.0f;
        [SerializeField] private Ease m_colorChangeEase = Ease.Linear;
        
        private Color m_currentColor;

        private Tween m_colorChangeTween;

        public Color CurrentColor
        {
            get => m_currentColor;
            set
            {
                m_currentColor = value;
                foreach (var graphic in m_sprites)
                {
                    graphic.color = m_currentColor;
                }
            }
        }

        private void Awake()
        {
            m_sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            if(m_colors.Length < 1) { return; }
            
            CurrentColor = m_colors[0];
        }

        public Tween ChangeColor(int _index)
        {
            if(m_colors.Length < 1) { return null; }
            
            _index = Math.Clamp(_index, 0, m_colors.Length);

            if (m_colorChangeTween is { active: true }) { m_colorChangeTween.Kill(); }

            m_colorChangeTween = DOTween.To(() => CurrentColor, _color => CurrentColor = _color, m_colors[_index], m_colorChangeTime).SetEase(m_colorChangeEase);
            return m_colorChangeTween;
        }
    }
}
