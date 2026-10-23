using UnityEngine;

namespace NPCControls
{
    public class UnitTargetingController : MonoBehaviour
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
            //This may be changed now, with a buffer distance to stop swapping
            //So if Player1 is 10 away and Player2 is 10.1 away, we would stay focused on Player1 until Player2 was ~8 away
            //This should keep the 'flicker' down, and the buffer limit can be adjusted as needed.
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
            if (_unitStats.GetIsMobile(gameObject))
            {
                if (!target)
                {
                    //Setup some sort of patrol path or something?
                    //Currently, all NPCs will be moving towards closest player at all times
                    //Only will need to change this if we want non-hostile NPCs or maybe NPCs staying in a certain zone
                    return;
                }
                _unitStats.GetUnitMovementController(gameObject).SetTarget(target);
                return;
            }
            
            //Currently leaving non-mobile NPC controls alone until mobile NPCs are working again
        }
    
       
    }
}
