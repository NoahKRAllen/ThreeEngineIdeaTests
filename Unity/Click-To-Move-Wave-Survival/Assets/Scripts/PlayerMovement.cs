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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        _moveClick = InputSystem.actions.FindAction("Move");
        _interactClick = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveClick.IsPressed())
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(mouseScreenPos), out var hit, 100))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
