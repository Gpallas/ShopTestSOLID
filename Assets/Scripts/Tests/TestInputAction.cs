using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputAction : MonoBehaviour
{
    public Item itemRef;
    public Inventory invAccess;
    // Use this for initialization
    void Start()
    {
        if (TryGetComponent(out ISubmitActionItem submitInterface))
        {
            submitInterface.AddOnSubmitAction(BuyItem, itemRef);
        }

        FindAnyObjectByType<PlayerInput>().SwitchCurrentActionMap("MyUI");
    }

    void BuyItem(Item itemToBuy)
    {
        int result = invAccess.TryToAddItem(itemToBuy);

        if (result < 0)
        {
            return;
        }

        Debug.Log("Bought " + itemToBuy.amount + " " + itemToBuy.data.name);
        Debug.Log("Current amount: " + invAccess.GetItem(itemToBuy).amount);
        Debug.Log("//////////////////////////////////");

        if (TryGetComponent(out IUpdateItem updateItemInterface))
        {
            updateItemInterface.UpdateItem(invAccess.GetItem(itemToBuy));
        }
    }
}
