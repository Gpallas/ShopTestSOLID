using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItemSubmitEventHandler : MonoBehaviour, ISubmitHandler, ISubmitActionItem, IUpdateItem
{
    Action<Item> onSubmitAction;
    Item item;

    public void OnSubmit(BaseEventData eventData)
    {
        onSubmitAction?.Invoke(item);
        Debug.Log(gameObject.name + " Submit");
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
