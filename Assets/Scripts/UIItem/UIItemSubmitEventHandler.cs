using System;
using UnityEngine;
using UnityEngine.EventSystems;

//NOT BEING USED ANYWHERE
public class UIItemSubmitEventHandler : MonoBehaviour, ISubmitHandler, ISubmitActionItem, IUpdateItem
{
    Action<Item> onSubmitAction;
    Item item;

    public void OnSubmit(BaseEventData eventData)
    {
        onSubmitAction?.Invoke(item);
    }

    public void AddOnSubmitAction(Action<Item> actionTotrigger, Item itemToTrigger)
    {
        onSubmitAction += actionTotrigger;
        item = itemToTrigger;
    }

    public void UpdateItem(Item newItem)
    {
        item = newItem;
    }
}
