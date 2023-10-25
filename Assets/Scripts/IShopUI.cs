using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopUI
{
    void PopulateShopkeeperMenu(IInventoryAccess shopkeeperInv);

    void PopulatePlayerMenu(IInventoryAccess playerInv);

    void FillBaseInfo(Sprite shopkeeperSprite, string shopkeeperString, int playerGold);

    void RefreshShopkeeperItems(IInventoryAccess shopkeeperInv);

    void RefreshPlayerItems(IInventoryAccess playerInv);

    void UpdateGoldValue(int newValue);

    void ClearItems();

    void SwitchVisibility(bool newVisibility);
}