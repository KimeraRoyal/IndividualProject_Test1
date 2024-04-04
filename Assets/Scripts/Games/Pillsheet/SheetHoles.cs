using IP1;
using UnityEngine;

public class SheetHoles : MonoBehaviour
{
    private Microgame m_microgame;
    
    private PillHole[] m_holes;
    
    [SerializeField] private Transform m_pillPrefab;

    private int m_pillsPopped;

    private void Awake()
    {
        m_microgame = GetComponentInParent<Microgame>();
        
        m_holes = GetComponentsInChildren<PillHole>();
    }

    private void Start()
    {
        for(var i = 0; i < m_holes.Length; i++)
        {
            var index = i;
            m_holes[i].OnPopped += () => { Pop(index); };
        }
    }

    private void Pop(int _index)
    {
        Instantiate(m_pillPrefab, m_holes[_index].transform.position, Quaternion.identity, transform);
        m_pillsPopped++;
        
        if(m_pillsPopped < 3) { return; }
        m_microgame.Clear();
    }
}
