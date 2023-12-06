using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class Movement : MonoBehaviour, IMovementInfo
{
    Rigidbody2D rb;

    bool canMove = true;
    float speed = 5f;
    Vector2 currentInput;
    Vector2 lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    public void UpdateMovementInput(Vector2 movementInput)
    {
        currentInput = movementInput;
    }

    public void SetCanMove(bool newValue)
    {
        canMove = newValue;
    }

    void MoveCharacter()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + (currentInput * speed * Time.fixedDeltaTime));

            if (currentInput.sqrMagnitude > 0)
            {
                lastDirection = currentInput.normalized;
            }
        }
    }
}
