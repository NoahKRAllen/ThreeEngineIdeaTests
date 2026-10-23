using UnityEngine;
using UnityEngine.AI;

namespace NPCControls
{
    public class UnitMovementController : MonoBehaviour
    {
        [SerializeField] private UnitStats unitStats;
        private NavMeshAgent _agent;
    
        //Only serialized to be seen in editor, not manually modified
        [SerializeField] private Transform _target;
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = unitStats.GetUnitMovementSpeed(gameObject);
        }

        void Update()
        {
            //Setup checks for reaching attack range, if range is reached then stop movement and start attacking
            //Can use enum like in ShapedLanes { Moving, Attacking }
            //Once in range, call over to UnitAttackController and set the attack bool to true to begin attacking
            _agent.SetDestination(_target.position);
        }
        public void SetTarget(Transform closest)
        {
            _target = closest;
        }
        
        
        
        
        
        
        private void OnDrawGizmosSelected()
        {
            if (!unitStats) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, unitStats.GetUnitAggroRange(gameObject));
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, unitStats.GetUnitAttackRange(gameObject));
        }
    }
}
