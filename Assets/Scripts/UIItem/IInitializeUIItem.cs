using System;

public interface IInitializeUIItem
{
    void Initialize(Item itemRef, IPopUpInfo popUp, Action<Item> onSubmitAction, Action<IPopUpInfo, Item> popUpConstruction);
}