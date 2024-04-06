using System.Linq;
using DG.Tweening;
using IP1;
using UnityEngine;

public class SheetHoles : MonoBehaviour
{
    private Microgame m_microgame;
    
    private PillHole[] m_holes;

    [SerializeField] private AlternatePosition m_leftArm;
    [SerializeField] private AlternatePosition m_rightArm;
    
    [SerializeField] private Transform m_pillPrefab;
    [SerializeField] private Transform m_fallingSheetPrefab;

    [SerializeField] private int m_pillsToClear = 3;
    [SerializeField] private float m_clearedArmOffset = 1.0f;
    [SerializeField] private float m_clearedArmMovementDuration = 1.0f;

    private int m_pillsPopped;

    public int PillsToClear
    {
        get => m_pillsToClear;
        set => m_pillsToClear = value;
    }

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
        
        if(m_pillsPopped >= m_pillsToClear) { m_microgame.Clear(); }
        
        CheckSheetEmpty();
    }

    private void CheckSheetEmpty()
    {
        if (m_holes.Any(hole => !hole.Open)) { return; }
        ReplaceSheet();
    }

    private void ReplaceSheet()
    {
        Instantiate(m_fallingSheetPrefab, transform.position, transform.rotation, m_microgame.transform);

        if (m_microgame.Cleared)
        {
            m_leftArm.AlternateOn = false;
            m_rightArm.AlternateOn = false;
            
            DOTween.To(() => m_leftArm.TargetPosition, _value => m_leftArm.TargetPosition = _value, m_leftArm.StartingPosition + Vector3.left * m_clearedArmOffset, m_clearedArmMovementDuration);
            DOTween.To(() => m_rightArm.TargetPosition, _value => m_rightArm.TargetPosition = _value, m_rightArm.StartingPosition + Vector3.right * m_clearedArmOffset, m_clearedArmMovementDuration);
            
            gameObject.SetActive(false);
        }
        else
        {
            foreach (var hole in m_holes)
            {
                hole.Open = false;
            }
        }
    }
}
