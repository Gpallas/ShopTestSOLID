using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class Movement : MonoBehaviour, IMovementInfo
{
    [SerializeField]
    Animator characterAnimator;
    Rigidbody2D rb;

    bool canMove = true;
    bool isMoving;

    [SerializeField]
    float speed;

    Vector2 movementValue;
    Vector2 lastDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    public void UpdateMovementValue(Vector2 updatedValue)
    {
        movementValue = updatedValue;
    }

    public void SetCanMove(bool newValue)
    {
        canMove = newValue;
    }

    public Vector2 GetLastMovementDirection()
    {
        return lastDirection;
    }

    void MoveCharacter()
    {
        isMoving = false;
        if (canMove)
        {

            if (movementValue.sqrMagnitude > 0)
            {
                rb.MovePosition(rb.position + (movementValue * speed * Time.fixedDeltaTime));

                isMoving = true;
                lastDirection = movementValue.normalized;

                characterAnimator.SetFloat("MovementX", movementValue.x);
                characterAnimator.SetFloat("MovementY", movementValue.y);
            }
        }

        characterAnimator.SetBool("IsMoving", isMoving);
    }


}
