using UnityEngine;

public interface IMovementInfo
{
    void UpdateMovementValue(Vector2 movementInput);

    void SetCanMove(bool newValue);

    Vector2 GetLastMovementDirection();
}