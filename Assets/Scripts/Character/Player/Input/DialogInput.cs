using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogInput : MonoBehaviour
{
    InputAction skipAction;

    void Start()
    {
        InputAssigner.assignCaller += Initialize;
        DialogSystem.instance.onDialogStart += DialogStarted;
        DialogSystem.instance.onDialogEnd += DialogEnded;
    }

    void Initialize(PlayerInput inputComponent)
    {
        skipAction = inputComponent.actions["SkipDialog"];
        InputAssigner.assignCaller -= Initialize;
    }

    void Skip(InputAction.CallbackContext obj)
    {
        DialogSystem.instance.SkipPressed();
    }

    void DialogStarted()
    {
        skipAction.Disable();
        skipAction.performed += Skip;
        skipAction.Enable();
    }

    void DialogEnded()
    {
        skipAction.performed -= Skip;
    }
}
