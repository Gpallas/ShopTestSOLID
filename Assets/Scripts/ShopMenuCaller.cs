using System;
using System.Collections;
using UnityEngine;

public abstract class ShopMenuCaller : MonoBehaviour
{
    protected Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string> menuCaller;
    abstract public void SetOpenMenuDelegate(Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string> openMenuMethod);
}
