using System.Collections;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    IPopUpInfo popUpRef;
    [SerializeField]
    IShopUI shopUIRef;

    IInventoryAccess playerInventory;
    IInventoryAccess shopkeeperInventory;

    IGoldAccess playerGold;

    void Start()
    {
        // Find Shopkeepers and register OpenMenu in their delegates
    }

    void OpenMenu(IInventoryAccess playerInvInterface, IInventoryAccess shopkeeperInvInterface, IGoldAccess goldInterface, Sprite shopkeeperSprite, string shopkeeperMessage)
    {
        playerInventory = playerInvInterface;
        shopkeeperInventory = shopkeeperInvInterface;
        playerGold = goldInterface;

        shopUIRef.FillBaseInfo(shopkeeperSprite, shopkeeperMessage, playerGold.GetCurrentGold());
        shopUIRef.PopulateShopkeeperMenu(shopkeeperInventory);
        shopUIRef.PopulatePlayerMenu(playerInventory);
        shopUIRef.SwitchVisibility(/*newVisibility = */true);
    }

    public void CloseMenu()
    {
        shopUIRef.ClearItems();
        shopUIRef.SwitchVisibility(/*newVisibility = */false);
    }

    //Acho que não precisa mais
    void ShowShopkeeperMenu()
    {

    }

    //Acho que não precisa mais
    void ClearShopkeeperMenu()
    {

    }

    //Acho que não precisa mais
    void ShowPlayerMenu()
    {

    }

    //Acho que não precisa mais
    void ClearPlayerMenu()
    {

    }

    void BuyItem(Item itemToBuy)//int itemIndex, int quantity)
    {
        //Item aux = itemToBuy;//shopkeeperInventory.GetItemAtIndex(itemIndex);

        bool wasSold = /*aux*/itemToBuy.data.wasSold;

        //Check if player has gold to buy one item
        if (playerGold.CheckHasEnoughGold(/*aux*/itemToBuy.data.goldValue))
        {
            //If player can buy the item at all, check if they have enough gold for the quantity they wanted. If not, switch to max they can afford
            int quantity = (playerGold.CheckHasEnoughGold(/*aux*/itemToBuy.data.goldValue */*quantity*/itemToBuy.amount)) ? /*quantity*/itemToBuy.amount : playerGold.GetCurrentGold()/itemToBuy.data.goldValue;
            /*aux*/itemToBuy.amount = quantity;

            //Try to add item to inventory
            int result = playerInventory.AddItem(/*aux*/itemToBuy);

            if (result < 0)
            {
                //Failed. Play "failed" sound effect
                return;
            }
            //Success. Play "success" sound effect

            int quantityBought = quantity - result;
            playerGold.RemoveGold(/*aux*/itemToBuy.data.goldValue * quantityBought);

            //TODO: Remove item from shopkeeper list IF it was an item sold by the player and rebought
            if (wasSold)
            {
                shopkeeperInventory.RemoveItemAmount(/*aux*/itemToBuy, quantityBought);
            }

            //Update menus
            shopUIRef.RefreshPlayerItems(playerInventory.GetItemList());
            shopUIRef.RefreshShopkeeperItems(shopkeeperInventory.GetItemList());
        }
    }

    void SellItem(Item itemToSell)//int itemIndex, int quantity)
    {
        int priorQuantity = playerInventory.GetItem(itemToSell).amount;

        //Item aux = itemToSell;//playerInventory.GetItemAtIndex(itemIndex);
        int result = playerInventory.RemoveItemAmount(/*aux*/itemToSell, /*quantity*/itemToSell.amount);

        if (result < 0)
        {
            //Failed. Play "failed" sound effect
            return;
        }
        //Success. Play "success" sound effect

        int quantitySold = (result == 0) ? /*aux.amount*/priorQuantity : /*quantity*/itemToSell.amount;
        playerGold.AddGold(/*aux*/itemToSell.data.goldValue * quantitySold);

        //TODO: Make items sold by the player appear as a separate item on the list that could be rebought in case of accidentally selling
        /*aux*/itemToSell.amount = quantitySold;
        /*aux*/itemToSell.data.wasSold = true;
        shopkeeperInventory.AddItemToFirstEmptySlot(/*aux*/itemToSell);

        //Update menus
        shopUIRef.RefreshPlayerItems(playerInventory.GetItemList());
        shopUIRef.RefreshShopkeeperItems(shopkeeperInventory.GetItemList());
    }

    //Mudar pro script de ShopUI
    public void ShowPopUp(int itemIndex, bool isShopkeeper)
    {
        if (isShopkeeper)
        {
            Item aux = shopkeeperInventory.GetItemAtIndex(itemIndex);
            popUpRef.ShowInfo(aux.data.itemName, aux.data.category.ToString(), aux.data.description, aux.data.goldValue.ToString());
        }
        else
        {
            Item aux = playerInventory.GetItemAtIndex(itemIndex);
            popUpRef.ShowInfo(aux.data.itemName, aux.amount, aux.data.goldValue.ToString());
        }
    }

    //Mudar pro script de ShopUI
    public void HidePopUp()
    {
        popUpRef.ClearPopUp();
    }
}