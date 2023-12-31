﻿using UnityEngine;

public class ShopManager : MonoBehaviour, ITradeItem
{
    public GameObject shopUIGO;

    IShopUI shopUIRef;

    IInventoryAccess playerInventory;
    IInventoryAccess shopkeeperInventory;

    IGoldAccess playerGold;

    IStateAccess<EPlayerState> playerState;

    void Start()
    {
        // Find Shopkeepers and register OpenMenu in their delegates
        ShopMenuCaller[] allCallers = FindObjectsByType<ShopMenuCaller>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (ShopMenuCaller caller in allCallers)
        {
            caller.SetOpenMenuDelegate(OpenMenu);
        }

        shopUIGO.TryGetComponent(out shopUIRef);
    }

    void OpenMenu(IInventoryAccess playerInvInterface, IInventoryAccess shopInvInterface, IGoldAccess goldInterface, Sprite shopSprite, string shopMessage, IStateAccess<EPlayerState> stateInterface)
    {
        playerInventory = playerInvInterface;
        shopkeeperInventory = shopInvInterface;
        playerGold = goldInterface;

        shopUIRef.SwitchVisibility(/*newVisibility = */true);
        shopUIRef.ReceiveTradeInterface(this);
        shopUIRef.FillBaseInfo(shopSprite, shopMessage, playerGold.GetCurrentGold());
        shopUIRef.PopulateShopkeeperMenu(shopkeeperInventory);
        shopUIRef.PopulatePlayerMenu(playerInventory);

        playerState = stateInterface;
        playerState.ChangeState(EPlayerState.OnShop);
    }

    public void CloseMenu()
    {
        shopUIRef.ClearItems();
        shopUIRef.SwitchVisibility(/*newVisibility = */false);

        shopkeeperInventory.InitializeInventory();

        playerState.ChangeState(EPlayerState.Default);
    }

    public void BuyItem(Item itemToBuy)
    {
        bool wasSold = itemToBuy.isPlayerItem;
        int itemValue = itemToBuy.GetItemValue();

        //Check if player has gold to buy one item
        if (playerGold.CheckHasEnoughGold(itemValue))
        {
            //If player can buy the item at all, check if they have enough gold for the quantity they wanted. If not, switch to max they can afford
            int quantity = (playerGold.CheckHasEnoughGold(itemValue * itemToBuy.amount)) ? itemToBuy.amount : playerGold.GetCurrentGold() / itemValue;

            //If player is rebuying an item, check if the amount the store stack has is smaller than the amount passed to buy. If not, switch to amount the store stack has
            if (wasSold)
            {
                int aux = shopkeeperInventory.GetItem(itemToBuy).amount;
                if (quantity > aux)
                {
                    quantity = aux;
                }
            }

            itemToBuy.amount = quantity;

            //Try to add item to inventory
            int result = playerInventory.TryToAddItem(itemToBuy);

            if (result < 0)
            {
                //Failed. Play "failed" sound effect
                return;
            }
            //Success. Play "success" sound effect

            //Update quantity of actually bought items and remove the equivalent gold
            int quantityBought = quantity - result;
            playerGold.RemoveGold(itemValue * quantityBought);

            //If player was rebuying item, remove the amount bought from the store stack
            if (wasSold)
            {
                shopkeeperInventory.RemoveItemAmount(itemToBuy, quantityBought);
            }

            //Update menus
            shopUIRef.RefreshPlayerItems(playerInventory);
            shopUIRef.RefreshShopkeeperItems(shopkeeperInventory);

            shopUIRef.UpdateGoldValue(playerGold.GetCurrentGold());
        }
        else
        {
            //Failed. Play "failed" sound effect
            return;
        }
    }

    public void SellItem(Item itemToSell)
    {
        int priorQuantity = playerInventory.GetItem(itemToSell).amount;

        int result = playerInventory.RemoveItemAmount(itemToSell, itemToSell.amount);

        if (result < 0)
        {
            //Failed. Play "failed" sound effect
            return;
        }
        //Success. Play "success" sound effect

        int quantitySold = (result == 0) ? priorQuantity : itemToSell.amount;
        playerGold.AddGold(itemToSell.GetItemValue() * quantitySold);

        itemToSell.amount = quantitySold;
        itemToSell.isPlayerItem = true;
        
        shopkeeperInventory.TryToAddItem(itemToSell);

        //Update menus
        shopUIRef.RefreshPlayerItems(playerInventory);
        shopUIRef.RefreshShopkeeperItems(shopkeeperInventory);

        shopUIRef.UpdateGoldValue(playerGold.GetCurrentGold());
    }
}
