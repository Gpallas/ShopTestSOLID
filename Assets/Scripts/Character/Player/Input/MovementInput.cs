using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    InputAction movementAction;

    IMovementInfo movementInfo;

    void Awake()
    {
        TryGetComponent(out movementInfo);
        InputAssigner.assignCaller += AssignMovement;
        InputAssigner.unassignCaller += UnassignMovement;
    }

    void AssignMovement(PlayerInput inputComponent)
    {
        if (movementAction == null)
        {
            movementAction = inputComponent.actions["Move"];
        }

        movementAction.started += UpdateMovement;
        movementAction.performed += UpdateMovement;
        movementAction.canceled += UpdateMovement;
    }

    void UnassignMovement()
    {
        movementAction.started -= UpdateMovement;
        movementAction.performed -= UpdateMovement;
        movementAction.canceled -= UpdateMovement;
    }

    void UpdateMovement(InputAction.CallbackContext obj)
    {
        movementInfo.UpdateMovementValue(obj.ReadValue<Vector2>());
    }
}
