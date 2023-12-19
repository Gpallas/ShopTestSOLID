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
        popUpRef.AddGoldAmount(itemRef.GetItemValue());
    }
}
