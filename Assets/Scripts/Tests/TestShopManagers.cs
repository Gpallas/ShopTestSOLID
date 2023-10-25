using System;
using System.Collections;
using UnityEngine;

public class TestShopManagers : ShopMenuCaller
{
    public ShopManager shopRef;
    public ShopUIManager shopUIRef;

    public Inventory playerInv;
    public ShopkeeperInventory shopkeeperInv;
    public GoldHandler playerGold;

    public Sprite shopkeeperImage;
    public string shopkeeperMessage;

    public override void SetOpenMenuDelegate(Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string> openMenuMethod)
    {
        menuCaller = openMenuMethod;
    }

    void Start()
    {
        StartCoroutine(StartTest());
    }

    IEnumerator StartTest()
    {
        yield return new WaitForSeconds(1f);

        menuCaller?.Invoke(playerInv, shopkeeperInv, playerGold, shopkeeperImage, shopkeeperMessage);
        
        yield return new WaitForSeconds(10f);

        shopRef.CloseMenu();

        yield return new WaitForSeconds(1f);

        menuCaller?.Invoke(playerInv, shopkeeperInv, playerGold, shopkeeperImage, shopkeeperMessage);
    }
}
