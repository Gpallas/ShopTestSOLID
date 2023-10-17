using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopUI
{
    void PopulateShopkeeperMenu(IInventoryAccess shopkeeperInv/*List<Item> itemList*/);

    void PopulatePlayerMenu(IInventoryAccess playerInv/*List<Item> itemList*/);

    void FillBaseInfo(Sprite shopkeeperSprite, string shopkeeperString, int playerGold);

    void RefreshShopkeeperItems(List<Item> itemList);

    void RefreshPlayerItems(List<Item> itemList);

    void UpdateGoldValue(int newValue);

    void ClearItems();

    void SwitchVisibility(bool newVisibility);
}