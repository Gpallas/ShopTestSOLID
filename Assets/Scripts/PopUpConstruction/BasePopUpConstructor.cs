using UnityEditor;
using UnityEngine;

public abstract class BasePopUpConstructor : ScriptableObject
{
    public void ConstructPopUp(IPopUpInfo popUpRef, Item itemRef) 
    {
        BuildPopUp(popUpRef, itemRef);
        popUpRef.AdjustSize();
    }

    public void ConstructPopUpWithGold(IPopUpInfo popUpRef, Item itemRef)
    {
        BuildPopUp(popUpRef, itemRef);
        popUpRef.AddGoldAmount(itemRef.amount * itemRef.data.goldValue);
        popUpRef.AdjustSize();
    }

    protected abstract void BuildPopUp(IPopUpInfo popUpRef, Item itemRef);
}
