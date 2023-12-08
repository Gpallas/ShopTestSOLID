using System.Collections;
using UnityEngine;

public interface IStateAccess
{
    void ChangeState(EPlayerState newState);

    EPlayerState GetCurrentState();
}