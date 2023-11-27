using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

//TODO: Mudar Os Item pra ItemClass e talvez fazer com que os Gets devolvam uma cópia, não uma referência

public class Inventory : MonoBehaviour, IInventoryAccess
{
    List<Item> itemList;
    int listSize = 36;

    public Item[] testItems;

    // Start is called before the first frame update
    void Start()
    {
        //This should be called on a load from somewhere
        //InitializeInventory();
        InitializeInventory(testItems);
    }

    public void InitializeInventory()
    {
        itemList = new List<Item>(listSize);

        for (int i = 0; i < listSize; i++)
        {
            itemList.Add(null);
        }
    }

    void InitializeInventory(Item[] items)
    {
        InitializeInventory();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data == null)
            {
                itemList[i] = null;
            }
            else
            {
                itemList[i] = items[i];
            }
        }
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

    public Item GetItem(Item itemRef)
    {
        if (itemList.Contains(itemRef))
        {
            return new Item(itemList[itemList.IndexOf(itemRef)]);
        }
        return null;
    }

    public Item GetItemWithData(ItemData dataRef)
    {
        int aux = IndexOfFirst(dataRef);
        if (IsListIndexValid(aux))
        {
            return itemList[aux];
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    public int TryToAddItem(Item itemToAdd)
    {
        int itemIndex = IndexOfFirstAvailable(itemToAdd.data);
        if (IsListIndexValid(itemIndex))
        {
            if (itemToAdd.data.stackable)
            {
                int leftovers = itemList[itemIndex].AddAmount(itemToAdd.amount);
                if (leftovers > 0)
                {
                    itemToAdd.amount = leftovers;
                    return AddItemToFirstEmptySlot(itemToAdd);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return AddItemToFirstEmptySlot(itemToAdd);
            }
        }
        else
        {
            int leftovers = itemToAdd.amount;
            int result = AddItemToFirstEmptySlot(itemToAdd);

            //If couldn't find an open slot, no item was added. Return -1
            if (result == leftovers)
            {
                return -1;
            }
            else
            {
                return result;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    int AddItemToIndex(Item itemToAdd, int index)
    {
        if (IsListIndexValid(index))
        {
            if (itemList[index] == null)
            {
                itemList[index] = new Item(itemToAdd);
                return 0;
            }
            else if (itemList[index].data.Equals(itemToAdd.data) && itemToAdd.data.stackable)
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
    int AddItemToFirstEmptySlot(Item itemToAdd)
    {
        int leftovers = itemToAdd.amount;
        for (int i=0; i<itemList.Count; i++)
        {
            if (itemList[i] == null)
            {
                leftovers = (itemToAdd.amount > itemToAdd.data.stackLimit) ? itemToAdd.amount - itemToAdd.data.stackLimit : 0;
                itemToAdd.amount -= leftovers;

                itemList[i] = new Item(itemToAdd, i);
                
                if (leftovers > 0)
                {
                    itemToAdd.amount = leftovers;
                    return AddItemToFirstEmptySlot(itemToAdd);
                }
                else
                {
                    return 0;
                }
            }
        }
        return leftovers;
    }

    void RemoveItem(Item itemToRemove)
    {
        int itemIndex = itemList.IndexOf(itemToRemove);
        if (IsListIndexValid(itemIndex))
        {
            if (!itemList[itemIndex].RemoveAmount(itemToRemove.amount))
            {
                RemoveItem(itemIndex);
            }
        }
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
        for (int i=0; i<itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (dataRef == itemList[i].data)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int IndexOfFirstAvailable(ItemData dataRef)
    {
        for (int i=0; i<itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (dataRef == itemList[i].data && itemList[i].amount < dataRef.stackLimit)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
