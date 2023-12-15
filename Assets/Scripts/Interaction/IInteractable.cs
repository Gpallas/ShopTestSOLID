using System.Collections;
using UnityEngine;

public interface IInteractable
{
    bool TryInteract(GameObject interactorGO);
}
