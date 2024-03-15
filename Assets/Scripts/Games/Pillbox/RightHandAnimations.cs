using System;
using UnityEngine;

namespace IP1.Games.Pillbox
{
    [RequireComponent(typeof(Animator))]
    public class RightHandAnimations : MonoBehaviour
    {
        private Animator m_animator;

        [SerializeField] private string m_grippingParameterName = "Gripping";
        
        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnGrippingChanged(bool _gripping)
        {
            m_animator.SetBool(m_grippingParameterName, _gripping);
        }
    }
}
