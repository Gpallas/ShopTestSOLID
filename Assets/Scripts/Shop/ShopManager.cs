using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopManager : MonoBehaviour, ITradeItem
{
    public GameObject shopUIGO;

    IShopUI shopUIRef;

    IInventoryAccess playerInventory;
    IInventoryAccess shopkeeperInventory;

    IGoldAccess playerGold;

    MyPlayerInput inputComponent;

    void Start()
    {
        // Find Shopkeepers and register OpenMenu in their delegates
        ShopMenuCaller[] allCallers = FindObjectsByType<ShopMenuCaller>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (ShopMenuCaller caller in allCallers)
        {
            caller.SetOpenMenuDelegate(OpenMenu);
        }

        shopUIGO.TryGetComponent(out shopUIRef);

        inputComponent = FindAnyObjectByType<MyPlayerInput>();
    }

    void OpenMenu(IInventoryAccess playerInvInterface, IInventoryAccess shopkeeperInvInterface, IGoldAccess goldInterface, Sprite shopkeeperSprite, string shopkeeperMessage)
    {
        playerInventory = playerInvInterface;
        shopkeeperInventory = shopkeeperInvInterface;
        playerGold = goldInterface;

        shopUIRef.SwitchVisibility(/*newVisibility = */true);
        shopUIRef.FillBaseInfo(shopkeeperSprite, shopkeeperMessage, playerGold.GetCurrentGold());
        shopUIRef.PopulateShopkeeperMenu(shopkeeperInventory);
        shopUIRef.PopulatePlayerMenu(playerInventory);

        inputComponent.SwitchActionMap(MyPlayerInput.myUIMapName);
    }

    public void CloseMenu()
    {
        shopUIRef.ClearItems();
        shopUIRef.SwitchVisibility(/*newVisibility = */false);

        shopkeeperInventory.InitializeInventory();

        inputComponent.SwitchActionMap(MyPlayerInput.defaultMapName);
    }

    public void BuyItem(Item itemToBuy)
    {
        bool wasSold = itemToBuy.isPlayerItem;
        int itemValue = (wasSold) ? itemToBuy.data.goldValue / 2 : itemToBuy.data.goldValue;

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
        playerGold.AddGold(itemToSell.data.goldValue / 2 * quantitySold);

        itemToSell.amount = quantitySold;
        itemToSell.isPlayerItem = true;
        
        shopkeeperInventory.TryToAddItem(itemToSell);

        //Update menus
        shopUIRef.RefreshPlayerItems(playerInventory);
        shopUIRef.RefreshShopkeeperItems(shopkeeperInventory);

        shopUIRef.UpdateGoldValue(playerGold.GetCurrentGold());
    }
}
