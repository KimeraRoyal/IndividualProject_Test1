using UnityEngine;

namespace IP1.Games.Pillbox
{
    [RequireComponent(typeof(Animator))]
    public class RightHand : MonoBehaviour
    {
        private Animator m_animator;

        private bool m_gripping;

        [SerializeField] private string m_grippingParameterName = "Gripping";
        
        private void Awake()
        {
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var gripping = Input.GetMouseButton(0);
            if (gripping == m_gripping) { return; }
            
            m_gripping = gripping;
            OnGrippingChanged(m_gripping);
        }

        private void OnGrippingChanged(bool _gripping)
        {
            m_animator.SetBool(m_grippingParameterName, _gripping);
        }
    }
}
