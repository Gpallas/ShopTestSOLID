using System.Collections;
using UnityEngine;

public class MessageInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    string[] messageList;

    IStateAccess playerState;

    public bool TryInteract(GameObject interactorGO)
    {
        DialogSystem.instance.StartDialog(messageList);
        DialogSystem.instance.onDialogEnd += ResetState;

        if (interactorGO.TryGetComponent(out playerState))
        {
            playerState.ChangeState(EPlayerState.Interacting);
        }
        return true;
    }

    void ResetState()
    {
        DialogSystem.instance.onDialogEnd -= ResetState;

        if (playerState != null)
        {
            playerState.ChangeState(EPlayerState.Default);
        }
    }
}
