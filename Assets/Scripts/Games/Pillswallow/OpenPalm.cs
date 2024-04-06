using System.Collections;
using DG.Tweening;
using IP1.Interaction;
using UnityEngine;

namespace IP1
{
    [RequireComponent(typeof(Mover), typeof(Rotator), typeof(MouseMovement))]
    public class OpenPalm : MonoBehaviour
    {
        private Microgame m_microgame;
        
        private Mover m_mover;
        private Rotator m_rotator;
        
        private MouseMovement m_mouseMovement;

        private SpriteAnimationSet[] m_animationSet;

        [SerializeField] private Transform m_pillPrefab;
        [SerializeField] private Transform m_pillSpawnPoint;
        [SerializeField] private Vector3 m_pillSpawnMinOffset, m_pillSpawnMaxOffset;
        [SerializeField] private float m_pillSpawnInterval = 1.0f;
        
        [SerializeField] private GameObject m_colliders;

        [SerializeField] private float m_dropZ;
        [SerializeField] private float m_dropSpriteHoldTime = 1.0f;

        [SerializeField] private float m_moveInSpeed = 1.0f;
        [SerializeField] private float m_moveOutSpeed = 1.0f;

        [SerializeField] private float m_rotateArmSpeed = 1.0f;
        
        private void Awake()
        {
            m_microgame = GetComponentInParent<Microgame>();
            
            m_mover = GetComponent<Mover>();
            m_rotator = GetComponent<Rotator>();
            
            m_mouseMovement = GetComponent<MouseMovement>();

            m_animationSet = GetComponentsInChildren<SpriteAnimationSet>();
        }

        private void Start()
        {
            m_mouseMovement.OnMouseMoved += OnMouseMoved;

            SpawnPills(3);
        }

        private void Update()
        {
            Drop();
        }

        private void OnMouseMoved(Vector2 _movement)
        {
            var speed = _movement.y > 0 ? m_moveOutSpeed : m_moveInSpeed;
            m_mover.TargetPosition += Vector3.forward * (_movement.y * speed);
            m_rotator.TargetRotation += Vector3.forward * (-_movement.x * m_rotateArmSpeed);
        }

        public void SpawnPills(int _count)
        {
            StartCoroutine(SpawnPillLoop(_count));
        }

        private IEnumerator SpawnPillLoop(int _count)
        {
            for (var i = 0; i < _count; i++)
            {
                if(i > 0) { yield return new WaitForSeconds(m_pillSpawnInterval); }

                var position = new Vector3();
                for (var axis = 0; axis < 3; axis++)
                {
                    position[axis] = Random.Range(m_pillSpawnMinOffset[axis], m_pillSpawnMaxOffset[axis]);
                }
                Instantiate(m_pillPrefab, m_pillSpawnPoint.position + position, Quaternion.identity, m_pillSpawnPoint);
            }
        }

        private void Drop()
        {
            if(!m_colliders.activeInHierarchy || transform.localPosition.z > m_dropZ) { return; }

            SetHandAnimation(1);
            m_colliders.SetActive(false);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(m_dropSpriteHoldTime);
            sequence.AppendCallback(() => { SetHandAnimation(0); });
            
            m_microgame.Clear();
        }

        private void SetHandAnimation(int _index)
        {
            foreach (var animationSet in m_animationSet)
            {
                animationSet.CurrentAnimationIndex = _index;
            }
        }
    }
}
