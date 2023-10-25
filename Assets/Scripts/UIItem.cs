using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIItem : MonoBehaviour, IInitializeUIItem, IUpdateItem
{
    protected Item item;

    IPopUpInfo popUpRef;

    public void Initialize(Item itemRef, IPopUpInfo popUp, Action<Item> onSubmitAction)
    {
        item = itemRef;
        popUpRef = popUp;

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
        item.data.constructorRef.ConstructPopUp(popUpRef, item);
    }

    void HidePopUp()
    {
        popUpRef.ClearPopUp();
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
    }
}