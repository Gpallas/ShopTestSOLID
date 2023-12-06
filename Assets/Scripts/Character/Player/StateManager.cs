using System.Collections;
using UnityEngine;

public enum EState
{
    Default,
    OnMenu,
    Interacting,
    OnShop
}
public class StateManager : MonoBehaviour, IStateAccess
{
    EState currentState;

    public void ChangeState(EState newState)
    {
        currentState = newState;
    }

    public EState GetCurrentState()
    {
        return currentState;
    }
}
