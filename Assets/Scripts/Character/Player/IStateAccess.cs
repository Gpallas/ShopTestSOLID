using System.Collections;
using UnityEngine;

public interface IStateAccess
{
    void ChangeState(EState newState);

    EState GetCurrentState();
}