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
                    //Check here on hit to see if we clicked on an enemy unit, if so SetDestination to enemy unit instead of hit.point
                    //if clicking on enemy unit, setup bool for active targeting, so that once we are within attack range, player stops instead of running up to the target like normal movement
                    
                    
                    _agent.SetDestination(hit.point);
                    //Once destination is within certain distance, .2f or something, set moving bool back off and stop player movement
                }
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
