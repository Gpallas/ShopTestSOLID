using System.Collections.Generic;

public interface IInventoryAccess
{
    void InitializeInventory();

    Item GetItemAtIndex(int index);
    Item GetItem(Item itemRef);
    Item GetItemWithData(ItemData dataRef);

    List<Item> GetItemList();

    int GetListCount();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <returns>
    /// Returns -1 if failed to add item. Returns 0 if added successfully. Otherwise, returns amount over stack limit
    /// </returns>
    int TryToAddItem(Item itemToAdd);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemIndex"></param>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns -1 if item not valid. Returns 0 if no amount was left. Otherwise, returns amount left
    /// </returns>
    int RemoveItemAmount(Item itemToRemove, int amountToRemove);

    void SwapItems(int firstIndex, int secondIndex);
}