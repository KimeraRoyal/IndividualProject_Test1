using IP1.Interaction;
using UnityEngine;

namespace IP1.Games.Pillbox
{
    [RequireComponent(typeof(Animator), typeof(MouseClick))]
    public class RightHandAnimations : MonoBehaviour
    {
        private Animator m_animator;
        private MouseClick m_mouseClick;

        [SerializeField] private string m_grippingParameterName = "Gripping";
        
        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_mouseClick = GetComponent<MouseClick>();
        }

        private void Start()
        {
            m_mouseClick.OnClickingChanged += OnGrippingChanged;
        }

        private void OnGrippingChanged(bool _gripping)
        {
            m_animator.SetBool(m_grippingParameterName, _gripping);
        }
    }
}
