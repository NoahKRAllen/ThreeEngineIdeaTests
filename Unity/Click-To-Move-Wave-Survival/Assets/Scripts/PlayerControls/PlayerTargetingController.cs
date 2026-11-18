using UnityEngine;

namespace PlayerControls
{
    public class PlayerTargetingController : MonoBehaviour
    {
        private float _scanTimer;
        [Tooltip("How often (seconds) to re-scan for targets. Lower = more responsive, more expensive.")]
        [SerializeField] float targetScanInterval;
        [Tooltip("Only colliders on these layers count as valid targets.")]
        [SerializeField] private LayerMask enemyLayerMask;
    
        private static readonly Collider[] ScanBuffer = new Collider[16];
    
        private PlayerStats _playerStats;

        [SerializeField] private GameObject playerUpperBody;
        [SerializeField] private float lookSpeed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
            _scanTimer = targetScanInterval;
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
            //AutoTargeting will be stopped during player active targeting, set by player right-clicking on an enemy unit
            if (!_playerStats.GetActiveTargeting(gameObject))
            {
                if (!(_scanTimer <= 0f)) return;
                _scanTimer = targetScanInterval;
                ScanForTarget();
            }
        }

        void LateUpdate()
        {
            if (!_playerStats.GetPlayerTarget(gameObject)) return;
            var lookPos = _playerStats.GetPlayerTarget(gameObject).position - playerUpperBody.transform.position;
            if (lookPos.sqrMagnitude <= 0.001f) return;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            playerUpperBody.transform.rotation = Quaternion.RotateTowards(playerUpperBody.transform.rotation, rotation, Time.deltaTime * lookSpeed);
        }
        
    
        private void ScanForTarget()
        {
            // Keep the current target as long as it's alive and still in aggro range, rather than
            // re-picking "closest" every scan - avoids flickering between two nearby enemies.
            //Checking for target also will handle swapping targets when one had died, removing the need for that in CombatController
            var targetPointer = _playerStats.GetPlayerTarget(gameObject);
            if (targetPointer && Vector3.Distance(transform.position, targetPointer.position) <= _playerStats.GetPlayerAggroRange(gameObject))
            {
                if (Vector3.Distance(transform.position,targetPointer.position) <= _playerStats.GetPlayerAttackRange(gameObject) / 2f)
                {
                    _playerStats.GetPlayerCombatController(gameObject).StartAttack(gameObject, targetPointer);
                }
                return;
            }

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

            _playerStats.SetPlayerTarget(gameObject, closest);
        }
    }
}
