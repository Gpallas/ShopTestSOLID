using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using static ItemData;

[Serializable]
public class Item
{
    /*public Sprite image;
    public string itemName;
    public string description;
    public int goldValue;
    public bool canSell;
    public bool wasSold;

    public ItemCategory category;

    public bool stackable = true;
    public int amount;
    public int stackLimit;

    public BasePopUpConstructor constructorRef;

    ItemClass(Sprite image, string itemName, string description, int goldValue, bool canSell, bool wasSold, ItemCategory category, bool stackable, int amount, int stackLimit, BasePopUpConstructor constructorRef)
    {
        this.image = image;
        this.itemName = itemName;
        this.description = description;
        this.goldValue = goldValue;
        this.canSell = canSell;
        this.wasSold = wasSold;
        this.category = category;
        this.stackable = stackable;
        this.amount = amount;
        this.stackLimit = stackLimit;
        this.constructorRef = constructorRef;
    }

    ItemClass (Item itemRef)
    {
        this.image = itemRef.image;
        this.itemName = itemRef.itemName;
        this.description = itemRef.description;
        this.goldValue = itemRef.goldValue;
        this.canSell = itemRef.canSell;
        this.wasSold = itemRef.wasSold;
        this.category = itemRef.category;
        this.stackable = itemRef.stackable;
        this.amount = itemRef.amount;
        this.stackLimit = itemRef.stackLimit;
        this.constructorRef = itemRef.constructorRef;
    }*/

    public ItemData data;
    public int amount;

    public Item(ItemData itemRef, int amountRef)
    {
        data = itemRef;
        amount = amountRef;
    }

    public Item(Item classRef)
    {
        data = classRef.data;
        amount = classRef.amount;
    }

    public int AddAmount(int amountToAdd)
    {
        amount += amountToAdd;
        if (amount > data.stackLimit)
        {
            int result = amount - data.stackLimit;
            amount = data.stackLimit;

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
            return (data.itemName == i.data.itemName);
        }
    }

    public override int GetHashCode()
    {
        return data.itemName.GetHashCode();
    }
}
