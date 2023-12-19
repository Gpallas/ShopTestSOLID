using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperInventory : MonoBehaviour, IInventoryAccess
{
    List<Item> itemList;

    [SerializeField]
    List<Item> testList;

    void Start()
    {
        InitializeInventory();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns 0 if added successfully
    /// </returns>
    public int TryToAddItem(Item itemToAdd)
    {
        int itemIndex = IndexOfFirstResellSlotAvailable(itemToAdd.data);
        if (IsListIndexValid(itemIndex))
        {
            int leftovers = itemList[itemIndex].AddAmount(itemToAdd.amount);
            if (leftovers > 0)
            {
                itemToAdd.amount = leftovers;
                AddItemToFirstEmptySlot(itemToAdd);
            }      
            return 0;
        }
        else
        {
            AddItemToFirstEmptySlot(itemToAdd);
            return 0;
        }
    }

    void AddItemToFirstEmptySlot(Item itemToAdd)
    {
        int leftovers;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == null)
            {
                leftovers = (itemToAdd.amount > itemToAdd.data.stackLimit) ? itemToAdd.amount - itemToAdd.data.stackLimit : 0;
                itemToAdd.amount -= leftovers;
                itemList[i] = new Item(itemToAdd, i);

                if (leftovers > 0)
                {
                    itemToAdd.amount = leftovers;
                }
                else
                {
                    return;
                }
            }
        }
        do
        {
            leftovers = (itemToAdd.amount > itemToAdd.data.stackLimit) ? itemToAdd.amount - itemToAdd.data.stackLimit : 0;
            itemToAdd.amount -= leftovers;

            itemList.Add(new Item(itemToAdd, itemList.Count));

            itemToAdd.amount = leftovers;
        }
        while (leftovers > 0);
    }

    public Item GetItem(Item itemRef)
    {
        if (itemList.Contains(itemRef))
        {
            return new Item(itemList[itemList.IndexOf(itemRef)]);
        }
        return null;
    }

    public Item GetItemAtIndex(int index)
    {
        if (IsListIndexValid(index))
        {
            if (itemList[index] != null)
            {
                return new Item(itemList[index]);
            }
        }
        return null;
    }

    public Item GetItemWithData(ItemData dataRef)
    {
        int aux = IndexOfFirst(dataRef);
        if (IsListIndexValid(aux))
        {
            return new Item(itemList[aux]);
        }
        return null;
    }

    public List<Item> GetItemList()
    {
        return new List<Item>(itemList);
    }

    public int GetListCount()
    {
        return itemList.Count;
    }

    void RemoveItem(int itemIndex)
    {
        itemList[itemIndex] = null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemIndex"></param>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns -1 if item not valid. Returns 0 if no amount was left. Otherwise, returns amount left
    /// </returns>
    public int RemoveItemAmount(Item itemToRemove, int amountToRemove)
    {
        int itemIndex = itemList.IndexOf(itemToRemove);

        return RemoveItemAmount(itemIndex, amountToRemove);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemIndex"></param>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns -1 if item index not valid. Returns 0 if no amount was left. Otherwise, returns amount left
    /// </returns>
    int RemoveItemAmount(int itemIndex, int amountToRemove)
    {
        if (IsListIndexValid(itemIndex))
        {
            if (itemList[itemIndex] != null)
            {
                int aux = (itemList[itemIndex].RemoveAmount(amountToRemove)) ? itemList[itemIndex].amount : 0;

                if (aux == 0)
                {
                    RemoveItem(itemIndex);
                }

                return aux;
            }
        }
        return -1;
    }

    public void SwapItems(int firstIndex, int secondIndex)
    {
        if (IsListIndexValid(firstIndex) && IsListIndexValid(secondIndex))
        {
            Item aux = itemList[firstIndex];
            itemList[firstIndex] = itemList[secondIndex];
            itemList[secondIndex] = aux;
        }
    }

    bool IsListIndexValid(int itemIndex)
    {
        return (itemIndex >= 0 && itemIndex < itemList.Count);
    }

    int IndexOfFirst(ItemData dataRef)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (dataRef == itemList[i].data && itemList[i].id != -1)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int IndexOfFirstResellSlotAvailable(ItemData dataRef)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (dataRef == itemList[i].data && itemList[i].id != -1)
                {
                    if (itemList[i].amount < itemList[i].data.stackLimit)
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }

    public void InitializeInventory()
    {
        itemList = new List<Item>(testList);
    }
}
