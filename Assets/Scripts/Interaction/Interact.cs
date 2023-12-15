using System.Collections;
using UnityEngine;

public class Interact : MonoBehaviour, IInteractor
{
    const string layerName = "Interactable";

    [SerializeField]
    Vector3 characterOffset;
    [SerializeField]
    float raycastLenght;

    bool canInteract;

    public void StartInteraction(GameObject interactorGO)
    {
        if (canInteract)
        {
            Vector2 raycastDirection = Vector2.up;

            if (interactorGO.TryGetComponent(out IMovementInfo movement))
            {
                raycastDirection = movement.GetLastMovementDirection();
                if (Mathf.Abs(raycastDirection.y) >= Mathf.Abs(raycastDirection.x))
                {
                    if (raycastDirection.y < 0)
                    {
                        raycastDirection = Vector2.down;
                    }
                    else
                    {
                        raycastDirection = Vector2.up;
                    }
                }
                else if (raycastDirection.x < 0)
                {
                    raycastDirection = Vector2.left;
                }
                else
                {
                    raycastDirection = Vector2.right;
                }
            }

            Debug.DrawLine(transform.position + characterOffset, transform.position + characterOffset + ((Vector3)raycastDirection*raycastLenght), Color.magenta, 10f);

            RaycastHit2D hit = Physics2D.Raycast(transform.position + characterOffset, raycastDirection, raycastLenght, 1 << LayerMask.NameToLayer(layerName));

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    //No objeto que interagir tem que fazer a mudança de estado do player
                    interactable.TryInteract(interactorGO);
                }
            }
        }
    }

    public void SetCanInteract(bool newValue)
    {
        canInteract = newValue;
    }
}
