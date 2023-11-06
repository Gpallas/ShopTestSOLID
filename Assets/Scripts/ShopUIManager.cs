using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour, IShopUI
{
    [SerializeField]
    GameObject popUpGO;
    [SerializeField]
    GameObject shopManagerGO;
    IPopUpInfo popUpRef;
    ITradeItem shopManagerRef;

    [SerializeField]
    GameObject shopUIGameObject;
    [SerializeField]
    Transform shopItemParent;
    [SerializeField]
    Transform playerItemParent;

    [SerializeField]
    Image shopkeeperImage;
    [SerializeField]
    TextMeshProUGUI shopkeeperMessage;
    [SerializeField]
    TextMeshProUGUI goldText;

    [SerializeField]
    GameObject shopItemPrefab;
    [SerializeField]
    GameObject playerItemPrefab;

    [SerializeField]
    BasePopUpConstructor playerItemPopUpConstructor;

    Dictionary<int, GameObject> shopkeeperItems;
    Dictionary<int, GameObject> playerItems;

    void Awake()
    {
        popUpGO.TryGetComponent(out popUpRef);
        shopManagerGO.TryGetComponent(out shopManagerRef);
    }

    public void PopulateShopkeeperMenu(IInventoryAccess shopkeeperInv)
    {
        shopkeeperItems = new Dictionary<int, GameObject>();

        for(int i=0; i<shopkeeperInv.GetListCount(); i++)
        {
            Item aux = shopkeeperInv.GetItemAtIndex(i);
            if (aux != null)
            {
                Action<IPopUpInfo, Item> actionAux = aux.data.constructorRef.ConstructPopUpWithGold;
                InstantiateItem(aux, shopItemPrefab, shopItemParent, ref shopkeeperItems, shopManagerRef.BuyItem, i, actionAux);
            }
        }
    }

    public void PopulatePlayerMenu(IInventoryAccess playerInv)
    {
        playerItems = new Dictionary<int, GameObject>();

        for (int i = 0; i < playerInv.GetListCount(); i++)
        {
            Item aux = playerInv.GetItemAtIndex(i);
            if (aux != null)
            {
                if (aux.data.canSell)
                {
                    InstantiateItem(aux, playerItemPrefab, playerItemParent, ref playerItems, shopManagerRef.SellItem, i, playerItemPopUpConstructor.ConstructPopUpWithGold);
                }
                else
                {
                    InstantiateItem(aux, playerItemPrefab, playerItemParent, ref playerItems, /*onSubmitAction = */null, i, /*popUpContructor = */null);
                }
            }
        }
    }

    public void ClearItems()
    {
        foreach (KeyValuePair<int, GameObject> g in shopkeeperItems)
        {
            Destroy(g.Value);
        }
        shopkeeperItems.Clear();
        foreach (KeyValuePair<int, GameObject> g in playerItems)
        {
            Destroy(g.Value);
        }
        playerItems.Clear();
    }

    public void FillBaseInfo(Sprite shopkeeperSprite, string shopkeeperString, int playerGold)
    {
        shopkeeperImage.sprite = shopkeeperSprite;
        shopkeeperMessage.text = shopkeeperString;
        UpdateGoldValue(playerGold);
    }

    public void RefreshPlayerItems(IInventoryAccess playerInv)
    {
        for (int i=0; i<playerInv.GetListCount(); i++)
        {
            Item aux = playerInv.GetItemAtIndex(i);
            if (aux != null)
            {
                if (playerItems.ContainsKey(i))
                {
                    if (playerItems[i].TryGetComponent(out IUpdateItem updateInterface))
                    {
                        updateInterface.UpdateItem(aux);
                    }
                }
                else
                {
                    if (aux.data.canSell)
                    {
                        InstantiateItem(aux, playerItemPrefab, playerItemParent, ref playerItems, shopManagerRef.SellItem, i, playerItemPopUpConstructor.ConstructPopUpWithGold);
                    }
                    else
                    {
                        InstantiateItem(aux, playerItemPrefab, playerItemParent, ref playerItems, /*onSubmitAction = */null, i, /*popUpContructor = */null);
                    }
                }
            }
            else
            {
                if (playerItems.ContainsKey(i))
                {
                    Destroy(playerItems[i]);
                    playerItems.Remove(i);
                }
            }
        }
    }

    public void RefreshShopkeeperItems(IInventoryAccess shopkeeperInv)
    {
        for (int i = 0; i < shopkeeperInv.GetListCount(); i++)
        {
            Item aux = shopkeeperInv.GetItemAtIndex(i);
            if (aux != null)
            {
                if (shopkeeperItems.ContainsKey(i))
                {
                    if (shopkeeperItems[i].TryGetComponent(out IUpdateItem updateInterface))
                    {
                        updateInterface.UpdateItem(aux);
                    }
                }
                else
                {
                    Action<IPopUpInfo, Item> actionAux = aux.data.constructorRef.ConstructPopUpWithGold;
                    InstantiateItem(new Item(aux), shopItemPrefab, shopItemParent, ref shopkeeperItems, shopManagerRef.BuyItem, i, actionAux);
                }
            }
            else
            {
                if (shopkeeperItems.ContainsKey(i))
                {
                    Destroy(shopkeeperItems[i]);
                    shopkeeperItems.Remove(i);
                }
            }
        }
    }

    public void SwitchVisibility(bool newVisibility)
    {
        shopUIGameObject.SetActive(newVisibility);
    }

    public void UpdateGoldValue(int newValue)
    {
        goldText.text = newValue.ToString();
    }

    void InstantiateItem(Item itemRef, GameObject itemPrefab, Transform itemParent, ref Dictionary<int, GameObject> dictionaryRef, 
                            Action<Item> submitAction, int index, Action<IPopUpInfo, Item> popUpConstructor)
    {
        GameObject instantiatedRef = Instantiate(itemPrefab, itemParent);

        if (instantiatedRef.TryGetComponent(out IInitializeUIItem initInterface))
        {
            initInterface.Initialize(itemRef, popUpRef, submitAction, popUpConstructor);
        }
        dictionaryRef.Add(index, instantiatedRef);
    }
}
