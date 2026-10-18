using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private UnitStats unitStats;
    private NavMeshAgent _agent;
    
    [SerializeField] private Transform _target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = unitStats.GetUnitMovementSpeed(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.position);
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
