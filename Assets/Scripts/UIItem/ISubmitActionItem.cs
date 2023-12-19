using System;

public interface ISubmitActionItem
{
    void AddOnSubmitAction(Action<Item> actionTotrigger, Item itemToTrigger);
}