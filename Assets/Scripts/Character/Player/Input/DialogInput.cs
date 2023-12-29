using UnityEngine;
using UnityEngine.InputSystem;

public class DialogInput : MonoBehaviour
{
    const string SkipDialogActionName = "SkipDialog";

    InputAction skipAction;

    void Awake()
    {
        InputAssigner.assignCaller += Initialize;
    }

    void Start()
    {
        DialogSystem.instance.onDialogStart += DialogStarted;
        DialogSystem.instance.onDialogEnd += DialogEnded;
    }

    void Initialize(PlayerInput inputComponent)
    {
        skipAction = inputComponent.actions[SkipDialogActionName];
        InputAssigner.assignCaller -= Initialize;
    }

    void Skip(InputAction.CallbackContext obj)
    {
        DialogSystem.instance.SkipPressed();
    }

    void DialogStarted()
    {
        //Necessary to do this, since skip and interact use the same button. I tried Disabling when not in use and only Enabling here, but it seems on map switch it also reenables
        skipAction.Disable();
        skipAction.performed += Skip;
        skipAction.Enable();
    }

    void DialogEnded()
    {
        skipAction.performed -= Skip;
    }
}
