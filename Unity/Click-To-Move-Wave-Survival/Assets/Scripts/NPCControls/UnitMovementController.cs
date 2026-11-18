using UnityEngine;
using UnityEngine.AI;

namespace NPCControls
{
    public class UnitMovementController : MonoBehaviour
    {
        private UnitStats _unitStats;
        private NavMeshAgent _agent;
        private bool _inRange;
        void Start()
        {
            _inRange = false;
            _unitStats = GetComponent<UnitStats>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _unitStats.GetUnitMovementSpeed(gameObject);
        }

        void Update()
        {
            //Setup checks for reaching attack range, if range is reached then stop movement and start attacking
            //Once in range, call over to UnitAttackController and set the attack bool to true to begin attacking
            
            if(_unitStats.GetUnitTarget(gameObject) && !_inRange)
                _agent.SetDestination(_unitStats.GetUnitTarget(gameObject).position);
            //Currently the only _agent.destination that the NPC units will have is the player character(s) and as such
            //instead of having a stopping distance of 0.5f like the player, _unitStats.AttackRange is used as the stopping point
            //The act of starting the attack is done inside the UnitTargetingController as it is the automatic targeting 
            //system that will handle it
            if (_unitStats.GetUnitTarget(gameObject) && 
                Vector3.Distance(transform.position, _agent.destination) <= _unitStats.GetUnitAttackRange(gameObject) / 2f)
            {
                _inRange = true;
                _agent.ResetPath();
            }
            else
            {
                _inRange = false;
            }
            
        }
        
        
        
        
        
        
        private void OnDrawGizmosSelected()
        {
            if (!_unitStats) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _unitStats.GetUnitAggroRange(gameObject));
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _unitStats.GetUnitAttackRange(gameObject));
        }
    }
}
