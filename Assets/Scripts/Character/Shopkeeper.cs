﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Shopkeeper : ShopMenuCaller, IInteractable
{
    [SerializeField]
    Sprite shopIcon;
    [SerializeField]
    string shopMessage;
    [SerializeField]
    string[] dialogBeforeShop;

    GameObject playerObj;

    public override void SetOpenMenuDelegate(Action<IInventoryAccess, IInventoryAccess, IGoldAccess, Sprite, string, IStateAccess<EPlayerState>> openMenuMethod)
    {
        menuCaller = openMenuMethod;
    }

    public bool TryInteract(GameObject interactorGO)
    {
        playerObj = interactorGO;
        if (dialogBeforeShop.Length > 0)
        {
            DialogSystem.instance.StartDialog(dialogBeforeShop);
            DialogSystem.instance.onDialogEnd += OpenMenu;

            if (interactorGO.TryGetComponent(out IStateAccess<EPlayerState> playerState))
            {
                playerState.ChangeState(EPlayerState.Interacting);
            }
        }
        else
        {
            OpenMenu();
        }
        return true;
    }

    void OpenMenu()
    {
        DialogSystem.instance.onDialogEnd -= OpenMenu;
        if (playerObj.TryGetComponent(out IStateAccess<EPlayerState> playerState))
        {
            if (playerObj.TryGetComponent(out IGoldAccess playerGold))
            {
                if (playerObj.TryGetComponent(out IInventoryAccess playerinv))
                {
                    if (TryGetComponent(out IInventoryAccess shopInv))
                    {
                        menuCaller?.Invoke(playerinv, shopInv, playerGold, shopIcon, shopMessage, playerState);
                        return;
                    }
                }
            }
            //If can't find the necessary interfaces to open the menu, change state back to Default so player can act normally
            playerState.ChangeState(EPlayerState.Default);
        }
    }
}
