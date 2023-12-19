using System;
using UnityEngine;

public abstract class UIItem : MonoBehaviour, IInitializeUIItem, IUpdateItem
{
    protected Item item;

    IPopUpInfo popUpRef;

    Action<IPopUpInfo, Item> constructor;
    bool isPopUpShowing = false;

    public void Initialize(Item itemRef, IPopUpInfo popUp, Action<Item> onSubmitAction, Action<IPopUpInfo, Item> popUpConstructor)
    {
        item = itemRef;
        popUpRef = popUp;

        constructor = popUpConstructor;

        PopulateItem();
        if (TryGetComponent(out ISelectAction selectInterface))
        {
            selectInterface.AddOnSelectAction(ShowPopUp);
        }
        if (TryGetComponent(out IDeselectAction deselectInterface))
        {
            deselectInterface.AddOnDeselectAction(HidePopUp);
        }
        if (TryGetComponent(out ISubmitActionItem submitInterface))
        {
            submitInterface.AddOnSubmitAction(onSubmitAction, itemRef);
        }
    }

    protected abstract void PopulateItem();

    protected virtual void ShowPopUp()
    {
        if (item != null)
        {
            isPopUpShowing = true;
            constructor?.Invoke(popUpRef, item);
        }
    }

    void HidePopUp()
    {
        if (isPopUpShowing)
        {
            isPopUpShowing = false;
            popUpRef.ClearPopUp();
        }
    }

    public void UpdateItem(Item newItem)
    {
        item = newItem;
        PopulateItem();

        IUpdateItem[] allUpdates = GetComponents<IUpdateItem>();
        TryGetComponent(out IUpdateItem thisInterface);

        foreach (IUpdateItem update in allUpdates)
        {
            if (!update.Equals(thisInterface))
            {
                update.UpdateItem(newItem);
            }
        }

        if (isPopUpShowing)
        {
            HidePopUp();
            ShowPopUp();
        }
    }
}