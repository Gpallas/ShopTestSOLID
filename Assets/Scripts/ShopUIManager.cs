using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour, IShopUI
{
    [SerializeField]
    IPopUpInfo popUpRef;

    [SerializeField]
    PlayerShopItemPopUpConstructor playerPopUpConstructor;

    IInventoryAccess playerInventory;
    IInventoryAccess shopkeeperInventory;

    [SerializeField]
    Image shopkeeperImage;
    [SerializeField]
    TextMeshProUGUI shopkeeperMessage;
    [SerializeField]
    TextMeshProUGUI goldText;
    [SerializeField]
    GameObject shopUIGameObject;

    [SerializeField]
    GameObject shopItemPrefab;
    [SerializeField]
    GameObject playerItemPrefab;

    //List<ShopItem> shopkeeperItems;
    //List<UIItem> playerItems;

    //Dictionary<int, GameObject> shopkeeperItems;
    //Dictionary<int, GameObject> playerItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateShopkeeperMenu(IInventoryAccess shopkeeperInv)
    {
        shopkeeperInventory = shopkeeperInv;

        for(int i=0; i<shopkeeperInventory.GetItemList().Count; i++)
        {

        }
    }

    public void PopulatePlayerMenu(IInventoryAccess playerInv)
    {
        playerInventory = playerInv;
    }

    public void ClearItems()
    {
        throw new System.NotImplementedException();
    }

    public void FillBaseInfo(Sprite shopkeeperSprite, string shopkeeperString, int playerGold)
    {
        shopkeeperImage.sprite = shopkeeperSprite;
        shopkeeperMessage.text = shopkeeperString;
        UpdateGoldValue(playerGold);
    }

    public void RefreshPlayerItems(List<Item> itemList)
    {
        //playerItems[i].GetComponent<IUpdateItem>().UpdateItem(item);
        throw new System.NotImplementedException();
    }

    public void RefreshShopkeeperItems(List<Item> itemList)
    {
        //shopkeeperItems[i].GetComponent<IUpdateItem>().UpdateItem(shopkeeperInventory.GetItemByID(shopkeeperItems[i].Key));
        throw new System.NotImplementedException();
    }

    public void SwitchVisibility(bool newVisibility)
    {
        shopUIGameObject.SetActive(newVisibility);
    }

    public void UpdateGoldValue(int newValue)
    {
        goldText.text = newValue.ToString();
    }

    public void ShowPopUp(int itemIndex, bool isShopkeeper)
    {
        if (isShopkeeper)
        {
            Item aux = shopkeeperInventory.GetItemAtIndex(itemIndex);
            aux.data.constructorRef.ConstructPopUp(popUpRef, aux);
            //popUpRef.ShowInfo(aux.itemName, aux.category.ToString(), aux.description, aux.goldValue.ToString());
        }
        else 
        {
            Item aux = playerInventory.GetItemAtIndex(itemIndex);
            if (aux.data.canSell)
            {
                playerPopUpConstructor.ConstructPopUp(popUpRef, aux);
            }
            //popUpRef.ShowInfo(aux.itemName, aux.amount, aux.goldValue.ToString());
        }
    }

    public void HidePopUp()
    {
        popUpRef.ClearPopUp();
    }
}
