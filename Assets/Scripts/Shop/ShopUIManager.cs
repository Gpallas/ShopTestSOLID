using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour, IShopUI
{
    const int maxItemsDisplayedOnAPage = 4;

    [SerializeField]
    GameObject popUpGO;
    IPopUpInfo popUpRef;
    ITradeItem shopManagerRef;

    [SerializeField]
    GameObject shopUIGameObject;
    [SerializeField]
    Transform shopItemParent;
    [SerializeField]
    Scrollbar shopItemScrollbar;
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
    }

    public void ReceiveTradeInterface(ITradeItem tradeInterfaceRef)
    {
        shopManagerRef = tradeInterfaceRef;
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

        shopItemScrollbar.numberOfSteps = shopkeeperItems.Count - (maxItemsDisplayedOnAPage - 1);
    }

    public void PopulatePlayerMenu(IInventoryAccess playerInv)
    {
        playerItems = new Dictionary<int, GameObject>();

        for (int i = 0; i < playerInv.GetListCount(); i++)
        {
            Item aux = playerInv.GetItemAtIndex(i);

            InstantiateItem(aux, playerItemPrefab, playerItemParent, ref playerItems, shopManagerRef.SellItem, i, playerItemPopUpConstructor.ConstructPopUpWithGold);
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

            if (playerItems[i].TryGetComponent(out IUpdateItem updateInterface))
            {
                updateInterface.UpdateItem(aux);
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

        shopItemScrollbar.numberOfSteps = shopkeeperItems.Count - (maxItemsDisplayedOnAPage - 1);
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
