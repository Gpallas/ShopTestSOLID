using System.Collections;
using UnityEngine;

public interface IInteractor
{
    void StartInteraction(GameObject interactorGO);
    void SetCanInteract(bool newValue);
}
