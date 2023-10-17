using System;
using System.Collections;
using UnityEngine;

public interface IInitializeUIItem
{
    void Initialize(Item itemRef, IPopUpInfo popUp, Action<Item> onSubmitAction);
}