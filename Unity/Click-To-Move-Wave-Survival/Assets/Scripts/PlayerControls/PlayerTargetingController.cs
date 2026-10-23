using UnityEngine;

namespace PlayerControls
{
    public class PlayerTargetingController : MonoBehaviour
    {
        private float _scanTimer;
        [Tooltip("How often (seconds) to re-scan for targets. Lower = more responsive, more expensive.")]
        [SerializeField] float targetScanInterval;
    
        [SerializeField] private Transform target;
    
        [Tooltip("Only colliders on these layers count as valid targets.")]
        [SerializeField] private LayerMask enemyLayerMask;
    
        private static readonly Collider[] ScanBuffer = new Collider[16];
    
        private PlayerStats _playerStats;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
        }

        // Update is called once per frame
        void Update()
        {
            //This will probably get a boolean set for not being active while player is directly controlling movement/targeting
            //This boolean will need to be turned on for movement/manual targeting and then turned back off for the following:
            //Movement destination reached
            //Enemy Target killed
            //Player Stop command input
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
            if (target && Vector3.Distance(transform.position, target.position) <= _playerStats.GetPlayerAggroRange(gameObject))
                return;

            int count = Physics.OverlapSphereNonAlloc(transform.position, _playerStats.GetPlayerAggroRange(gameObject), ScanBuffer, enemyLayerMask);

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
            //Currently non-mobile players aren't a thing, but they could be in the future. Like siege mode or something
            //if (_playerStats.GetIsMobile(gameObject))
            //{
                if (!target)
                {
                    //Player character shouldn't do anything without a target
                    return;
                }
                //This will need to be a check function not a straight SetTarget
                //We may not even do this to the PlayerMovementController, as the player should only be auto-targeting
                //not auto-moving
                //Could just set the player character to track/face targets within aggro range
                //This will of course need editing if/when I implement fog-of-war
                //_playerStats.GetPlayerMovementController(gameObject).SetTarget(target);
                return;
            //}
            
            //Currently leaving non-mobile NPC controls alone until mobile NPCs are working again
        }
    }
}
