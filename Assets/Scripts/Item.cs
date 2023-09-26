using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public enum ItemCategory
    {
        None
    }

    public Sprite image;
    public string itemName;
    public string description;
    public int goldValue;
    public bool canSell;
    public bool wasSold;

    public ItemCategory category;

    public bool stackable = true;
    public int amount;
    public int stackLimit;

    public int AddAmount(int amountToAdd)
    {
        amount += amountToAdd;
        if (amount > stackLimit)
        {
            int result = amount - stackLimit;
            amount = stackLimit;

            return result;
        }
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amountToRemove"></param>
    /// <returns>
    /// Returns true if amount left is greater than 0.
    /// </returns>
    public bool RemoveAmount(int amountToRemove)
    {
        amount = (amountToRemove > amount) ? 0 : amount - amountToRemove;

        return (amount > 0);
    }

    public override bool Equals(object other)
    {
        //Check for null and compare run-time types.
        if ((other == null) || !this.GetType().Equals(other.GetType()))
        {
            return false;
        }
        else
        {
            Item i = (Item)other;
            return (itemName == i.itemName);
        }
    }

    public override int GetHashCode()
    {
        return itemName.GetHashCode();
    }
}
