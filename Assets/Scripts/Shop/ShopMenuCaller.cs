using System;
using UnityEngine;

public abstract class ShopMenuCaller : MonoBehaviour
{
    protected Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string, IStateAccess> menuCaller;
    abstract public void SetOpenMenuDelegate(Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string, IStateAccess> openMenuMethod);
}
