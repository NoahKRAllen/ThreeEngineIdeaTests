using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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
            //Future plans:
            /*Check if raycast hits enemy units, if so target enemy unit as destination and focus targeting on said unit
             * Only run this if statement while player character is selected as left-clicked focused unit
             */
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(mouseScreenPos), out var hit, 100))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!playerStats) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerStats.GetPlayerAgroRange(gameObject));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, playerStats.GetPlayerAttackRange(gameObject));
    }
}
