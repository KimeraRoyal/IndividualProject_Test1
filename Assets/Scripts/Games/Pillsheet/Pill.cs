using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Pill : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    [SerializeField] private float m_minAngularVelocity;
    [SerializeField] private float m_maxAngularVelocity;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_rigidbody.angularVelocity = Random.Range(m_minAngularVelocity, m_maxAngularVelocity);
    }
}
