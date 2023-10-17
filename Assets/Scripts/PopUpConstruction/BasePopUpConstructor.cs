using UnityEditor;
using UnityEngine;

public class BasePopUpConstructor : ScriptableObject
{
    public virtual void ConstructPopUp(IPopUpInfo popUpRef, Item itemRef) { }
}
