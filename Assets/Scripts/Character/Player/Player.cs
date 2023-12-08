using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

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
        //StartCoroutine(ASDQWE());
    }

    IEnumerator ASDQWE()
    {
        PlayerStateManager playerState = FindAnyObjectByType<PlayerStateManager>();
        yield return new WaitForSeconds(4f);


        playerState.ChangeState(EPlayerState.Interacting);

        yield return new WaitForSeconds(2f);

        playerState.ChangeState(EPlayerState.OnShop);

        yield return new WaitForSeconds(1f);

        playerState.ChangeState(EPlayerState.Default);
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
