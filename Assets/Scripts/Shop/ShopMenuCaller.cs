using System;
using UnityEngine;

public abstract class ShopMenuCaller : MonoBehaviour
{
    protected Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string, IStateAccess<EPlayerState>> menuCaller;
    abstract public void SetOpenMenuDelegate(Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string, IStateAccess<EPlayerState>> openMenuMethod);
}
