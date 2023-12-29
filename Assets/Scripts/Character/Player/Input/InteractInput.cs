using UnityEngine;
using UnityEngine.InputSystem;

public class InteractInput : MonoBehaviour
{
    const string InteractActionName = "Interact";

    InputAction interactAction;

    IInteractor interactor;

    void Awake()
    {
        TryGetComponent(out interactor);
        InputAssigner.assignCaller += AssignInput;
        InputAssigner.unassignCaller += UnassignInput;
    }

    void AssignInput(PlayerInput inputComponent)
    {
        if (interactAction == null)
        {
            interactAction = inputComponent.actions[InteractActionName];
        }

        interactAction.performed += Interact;
    }

    void UnassignInput()
    {
        interactAction.performed -= Interact;
    }

    void Interact(InputAction.CallbackContext obj)
    {
        interactor.StartInteraction(gameObject);
    }
}
