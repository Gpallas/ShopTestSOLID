using System.Collections;
using UnityEngine;

public interface IMovementInfo
{
    void UpdateMovementInput(Vector2 movementInput);

    void SetCanMove(bool newValue);
}