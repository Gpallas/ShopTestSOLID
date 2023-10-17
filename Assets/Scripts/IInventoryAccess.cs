using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryAccess
{
    Item GetItemAtIndex(int index);
    Item GetItem(Item itemRef);

    List<Item> GetItemList();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    int AddItem(Item itemToAdd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    int AddItemToIndex(Item itemToAdd, int index);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns true if able to add item. False if there was no open slot
    /// </returns>
    bool AddItemToFirstEmptySlot(Item itemToAdd);

    void RemoveItem(Item itemToRemove);
    void RemoveItem(int itemIndex);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemIndex"></param>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns -1 if item not valid. Returns 0 if no amount was left. Otherwise, returns amount left
    /// </returns>
    int RemoveItemAmount(Item itemToRemove, int amountToRemove);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemIndex"></param>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns -1 if item index not valid. Returns 0 if no amount was left. Otherwise, returns amount left
    /// </returns>
    int RemoveItemAmount(int itemIndex, int amountToRemove);

    void SwapItems(int firstIndex, int secondIndex);
}