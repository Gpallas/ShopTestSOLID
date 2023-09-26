using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IInventoryAccess
{
    List<Item> itemList;
    int listSize;

    // Start is called before the first frame update
    void Start()
    {
        //This should be called on a load from somewhere
        InitializeInventory();
    }

    void InitializeInventory()
    {
        itemList = new List<Item>(listSize);
    }

    void InitializeInventory(Item[] items)
    {
        itemList = new List<Item>(listSize);
        itemList.AddRange(items);
    }

    public Item GetItemAtIndex(int index)
    {
        if (IsListIndexValid(index))
        {
            return itemList[index];
        }
        return null;
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    public int AddItem(Item itemToAdd)
    {
        int itemIndex = itemList.IndexOf(itemToAdd);
        if (IsListIndexValid(itemIndex))
        {
            if (itemList[itemIndex] == itemToAdd && itemToAdd.stackable)
            {
                int leftovers = itemList[itemIndex].AddAmount(itemToAdd.amount);
                if (leftovers > 0)
                {
                    itemToAdd.amount = leftovers;
                    if (AddItemToFirstEmptySlot(itemToAdd))
                    {
                        return 0;
                    }
                    return leftovers;
                }
            }
        }
        else
        {
            if (AddItemToFirstEmptySlot(itemToAdd))
            {
                return 0;
            }
        }
        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    public int AddItemToIndex(Item itemToAdd, int index)
    {
        if (IsListIndexValid(index))
        {
            if (itemList[index] == null)
            {
                itemList[index] = itemToAdd;
                return 0;
            }
            else if (itemList[index] == itemToAdd && itemToAdd.stackable)
            {
                return itemList[index].AddAmount(itemToAdd.amount);
            }
        }
        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns true if able to add item. False if there was no open slot
    /// </returns>
    public bool AddItemToFirstEmptySlot(Item itemToAdd)
    {
        for (int i=0; i<itemList.Count; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i] = itemToAdd;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item itemToRemove)
    {
        int itemIndex = itemList.IndexOf(itemToRemove);
        if (IsListIndexValid(itemIndex))
        {
            if (!itemList[itemIndex].RemoveAmount(itemToRemove.amount))
            {
                RemoveItem(itemIndex);
            }
        }
        //itemList[itemList.IndexOf(itemToRemove)] = null;
        //itemList.Remove(itemToRemove);
    }

    public void RemoveItem(int itemIndex)
    {
        itemList[itemIndex] = null;
        //itemList.RemoveAt(itemIndex);
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
        //if (IsListIndexValid(itemIndex))
        //{
        //    return (itemList[itemIndex].RemoveAmount(amountToRemove)) ? 1 : 0;
        //}
        //return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemIndex"></param>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns -1 if item index not valid. Returns 0 if no amount was left. Otherwise, returns amount left
    /// </returns>
    public int RemoveItemAmount(int itemIndex, int amountToRemove)
    {
        if (IsListIndexValid(itemIndex))
        {
            int aux = (itemList[itemIndex].RemoveAmount(amountToRemove)) ? itemList[itemIndex].amount : 0;

            if (aux == 0)
            {
                RemoveItem(itemIndex);
            }

            return aux;
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
}
