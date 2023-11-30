using UnityEditor;
using UnityEngine;

public abstract class BasePopUpConstructor : ScriptableObject
{
    public void ConstructPopUp(IPopUpInfo popUpRef, Item itemRef) 
    {
        BuildPopUp(popUpRef, itemRef);
        popUpRef.FinishPopUp();
    }

    public void ConstructPopUpWithGold(IPopUpInfo popUpRef, Item itemRef)
    {
        BuildPopUp(popUpRef, itemRef);
        BuildGoldEntry(popUpRef, itemRef);
        popUpRef.FinishPopUp();
    }

    protected abstract void BuildPopUp(IPopUpInfo popUpRef, Item itemRef);
    protected virtual void BuildGoldEntry(IPopUpInfo popUpRef, Item itemRef)
    {
        int value = (itemRef.isPlayerItem) ? itemRef.data.goldValue / 2 : itemRef.data.goldValue;
        popUpRef.AddGoldAmount(value);
    }
}
