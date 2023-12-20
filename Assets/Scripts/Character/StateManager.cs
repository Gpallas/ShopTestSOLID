using System;
using UnityEngine;

public class StateManager<T> : MonoBehaviour, IStateAccess<T> where T : IConvertible
{
    T currentState;

    [SerializeField]
    Animator stateAnimator;

    public void ChangeState(T newState)
    {
        currentState = newState;
        stateAnimator.SetInteger("State", Convert.ToInt16(currentState));
    }

    public T GetCurrentState()
    {
        return currentState;
    }
}
