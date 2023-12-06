using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    MyPlayerInput inputComponent;

    InputAction moveAction;

    IMovementInfo movement;

    void Start()
    {
        moveAction = inputComponent.actions["Move"];
        TryGetComponent(out movement);
        AssignAllActions();
    }

    void UpdateMovement(InputAction.CallbackContext obj)
    {
        movement.UpdateMovementInput(obj.ReadValue<Vector2>());
    }

    void AssignAllActions()
    {
        AssignMovementActions();
    }

    void AssignMovementActions()
    {
        moveAction.started += UpdateMovement;
        moveAction.performed += UpdateMovement;
        moveAction.canceled += UpdateMovement;
    }

    void UnassignAllActions()
    {
        UnassignMovementActions();
    }

    void UnassignMovementActions()
    {

        moveAction.started -= UpdateMovement;
        moveAction.performed -= UpdateMovement;
        moveAction.canceled -= UpdateMovement;
    }

    void OnDisable()
    {
        UnassignAllActions();
    }

    void OnDestroy ()
    {
        UnassignAllActions();
    }
}
