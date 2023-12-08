using System.Collections;
using UnityEngine;

public enum EPlayerState
{
    Default = 0,
    Interacting = 1,
    OnShop = 2
}
public class PlayerStateManager : MonoBehaviour, IStateAccess
{
    EPlayerState currentState;

    [SerializeField]
    Animator stateAnimator;

    public void ChangeState(EPlayerState newState)
    {
        currentState = newState;
        stateAnimator.SetInteger("State", (int)currentState);
    }

    public EPlayerState GetCurrentState()
    {
        return currentState;
    }
}
