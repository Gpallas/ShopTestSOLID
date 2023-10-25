using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestUIItem : MonoBehaviour
{
    public GameObject uiItemPrefab;

    public GameObject canvasRef;
    public Item itemRef;
    public GameObject popUpRef;
    public ShopManager shopRef;

    public Inventory playerInventory;
    public Inventory shopkeeperInventory;
    public GoldHandler playerGold;

    GameObject instantiatedRef;

    void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);

        playerInventory.TryToAddItem(itemRef);
        itemRef = playerInventory.GetItem(itemRef);

        instantiatedRef = Instantiate(uiItemPrefab, canvasRef.transform);

        if (instantiatedRef.TryGetComponent(out IInitializeUIItem initInterface))
        {
            initInterface.Initialize(itemRef, popUpRef.GetComponent<IPopUpInfo>(), SellItem);
        }

        FindAnyObjectByType<PlayerInput>().SwitchCurrentActionMap("MyUI");
    }

    public void SellItem(Item itemToSell)
    {
        int priorQuantity = playerInventory.GetItem(itemToSell).amount;

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
        /*aux*/
        itemToSell.amount = quantitySold;
        /*aux*/
        itemToSell.wasSold = true;
        shopkeeperInventory.TryToAddItem(/*aux*/itemToSell);

        if (result > 0 )
        {
            if (instantiatedRef.TryGetComponent(out IUpdateItem updateInterface))
            {
                updateInterface.UpdateItem(playerInventory.GetItem(itemToSell));
            }
        }
        else
        {
            Debug.Log("Must Destroy Item");
        }
    }
}
