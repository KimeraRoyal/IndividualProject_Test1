using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IP1
{
    [Serializable]
    public class RandomSpriteFrame
    {
        [SerializeField] private Sprite m_sprite;
        [SerializeField] private int m_weight = 1;

        private int m_totalWeight;

        public Sprite Sprite => m_sprite;
        public int Weight => m_weight;
        
        public int TotalWeight { get => m_totalWeight; set => m_totalWeight = value; }
    }
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomSprite : MonoBehaviour
    {
        private SpriteRenderer m_spriteRenderer;
        
        [SerializeField] private RandomSpriteFrame[] m_sprites;

        private int m_totalWeight;
        
        private void Awake()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();

            foreach (var sprite in m_sprites)
            {
                m_totalWeight += sprite.Weight;
                sprite.TotalWeight = m_totalWeight;
            }
        }

        private void Start()
        {
            var randomValue = Random.Range(0, m_totalWeight);
            foreach (var sprite in m_sprites)
            {
                if (randomValue >= sprite.TotalWeight) { continue; }
                m_spriteRenderer.sprite = sprite.Sprite;
                break;
            }
        }
    }
}
