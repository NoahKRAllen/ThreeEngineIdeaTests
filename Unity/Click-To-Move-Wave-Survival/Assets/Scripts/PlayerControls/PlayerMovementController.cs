using NPCControls;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace PlayerControls
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]PlayerStats playerStats;

        private NavMeshAgent _agent;
        
        InputAction _moveClick;
        InputAction _interactClick;
        private Camera _mainCamera;
        void Start()
        {
            _mainCamera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _moveClick = InputSystem.actions.FindAction("Move");
            _interactClick = InputSystem.actions.FindAction("Interact");
            _agent.speed = playerStats.GetPlayerMovementSpeed(gameObject);
        }

        void Update()
        {
            if (_moveClick.IsPressed())
            {
                //Set bool to turn off player auto-targeting in PlayerTargetingController for the duration that the player is manually targeting a unit and/or manually moving the character.
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(mouseScreenPos), out var hit, 100))
                {
                    playerStats.SetActiveTargeting(gameObject, false);
                    //Check here on hit to see if we clicked on an enemy unit, if so SetDestination to enemy unit instead of hit.point
                    //if clicking on enemy unit, setup bool for active targeting, so that once we are within attack range, player stops instead of running up to the target like normal movement

                    if (hit.collider.gameObject.TryGetComponent(out UnitStats unit))
                    {
                        playerStats.SetActiveTargeting(gameObject, true);
                        _agent.SetDestination(unit.transform.position);
                        playerStats.SetPlayerTarget(gameObject, unit.transform);
                    }
                    else
                    {
                        _agent.SetDestination(hit.point);
                    }
                    //Once destination is within certain distance, .2f or something, set moving bool back off and stop player movement
                }
            }

            if (!playerStats.GetPlayerTarget(gameObject))
            {
                playerStats.SetActiveTargeting(gameObject, false);
                return;
            }
            if (playerStats.GetActiveTargeting(gameObject))
            {
                if (Vector3.Distance(transform.position, playerStats.GetPlayerTarget(gameObject).position) <=
                    playerStats.GetPlayerAttackRange(gameObject))
                {
                    _agent.ResetPath();
                    playerStats.GetPlayerCombatController(gameObject)
                        .StartAttack(gameObject, playerStats.GetPlayerTarget(gameObject));
                }
                else
                {
                    _agent.SetDestination(playerStats.GetPlayerTarget(gameObject).position);
                }
            }
            else if(Vector3.Distance(transform.position, _agent.destination) <= 0.5f)
            {
                _agent.ResetPath();
            }
        }
    
        private void OnDrawGizmosSelected()
        {
            if (!playerStats) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, playerStats.GetPlayerAggroRange(gameObject));
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, playerStats.GetPlayerAttackRange(gameObject));
        }
    }
}
