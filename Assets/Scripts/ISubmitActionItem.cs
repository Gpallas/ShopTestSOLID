using System;
using System.Collections;
using UnityEngine;

public interface ISubmitActionItem
{
    void AddOnSubmitAction(Action<Item> actionTotrigger, Item itemToTrigger);
}