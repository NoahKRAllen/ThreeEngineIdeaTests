using UnityEditor;
using UnityEngine;

public class TargetingController : MonoBehaviour
{
    private float _scanTimer;
    [Tooltip("How often (seconds) to re-scan for targets. Lower = more responsive, more expensive.")]
    [SerializeField] float targetScanInterval;
    
    [SerializeField] private Transform target;
    
    [Tooltip("Only colliders on these layers count as valid targets.")]
    [SerializeField] private LayerMask enemyLayerMask;
    
    private static readonly Collider[] ScanBuffer = new Collider[16];
    
    private UnitStats _unitStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _unitStats = GetComponent<UnitStats>();
    }

    // Update is called once per frame
    void Update()
    {
        _scanTimer -= Time.deltaTime;
        if (_scanTimer <= 0f)
        {
            _scanTimer = targetScanInterval;
            ScanForTarget();
        }
    }
    
    private void ScanForTarget()
    {
        // Keep the current target as long as it's alive and still in range, rather than
        // re-picking "closest" every scan - avoids flickering between two nearby enemies.
        if (target && Vector3.Distance(transform.position, target.position) <= _unitStats.GetUnitAggroRange(gameObject))
            return;

        int count = Physics.OverlapSphereNonAlloc(transform.position, _unitStats.GetUnitAggroRange(gameObject), ScanBuffer, enemyLayerMask);

        Transform closest = null;
        float closestSqrDist = float.MaxValue;
        
        for (int i = 0; i < count; i++)
        {
            Vector3 closestPoint = ScanBuffer[i].ClosestPoint(transform.position);
            float sqrDist = (closestPoint - transform.position).sqrMagnitude;
            if (sqrDist < closestSqrDist)
            {
                closestSqrDist = sqrDist;
                closest = ScanBuffer[i].transform;
            }
        }

        target = closest;
    /*
     This needs to be updated to handle the movement using UnitMovement and PlayerMovement
        if (_unitStats.isMobile)
        {
            if (!target)
            {
                //print("No target, swapping to follow waypoints");
                _unit.unitMovementController.SetTarget(null);
                return;
            }

            //print($"Target found, setting attacking state to {target.name}");
            _unit.unitMovementController.SetTarget(target);
            return;
        }

        //TODO: Build new script for towers that handles the attack trigger that is currently done inside of UnitMovementController
        if (!target)
        {
            //print($"{gameObject.name} lost target, going passive");
            _unit.stationaryUnitController.SetTarget(null);
            return;
        }
        //print($"{gameObject.name} found target, attacking {target.name}");
        _unit.stationaryUnitController.SetTarget(target);
        */
    }
    
}
