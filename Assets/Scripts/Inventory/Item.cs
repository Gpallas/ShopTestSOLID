using System;

[Serializable]
public class Item
{
    const int playerItemValueModifier = 2;

    public ItemData data;
    public int amount;
    public bool isPlayerItem;
    public int id = -1;
    
    public Item()
    {
        data = null;
        amount = -1;
        isPlayerItem = false;
        id = -1;
    }

    public Item(ItemData dataRef)
    {
        data = dataRef;
        amount = 1;
        isPlayerItem = false;
        id = -1;
    }

    public Item(ItemData dataRef, bool isPlayer)
    {
        data = dataRef;
        amount = 1;
        isPlayerItem = isPlayer;
        id = -1;
    }

    public Item(ItemData dataRef, int amountRef, bool isPlayer, int idRef)
    {
        data = dataRef;
        amount = amountRef;
        isPlayerItem = isPlayer;
        id = idRef;
    }

    public Item(Item itemRef)
    {
        data = itemRef.data;
        amount = itemRef.amount;
        isPlayerItem = itemRef.isPlayerItem;
        id = itemRef.id;
    }

    public Item(Item itemRef, int idRef)
    {
        data = itemRef.data;
        amount = itemRef.amount;
        isPlayerItem = itemRef.isPlayerItem;
        id = idRef;
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

    public int GetItemValue(bool valueOfAllStack = false, bool considerPlayerItemValue = true)
    {
        int value = data.goldValue;
        if (valueOfAllStack)
        {
            value *= amount;
        }
        if (considerPlayerItemValue)
        {
            if (isPlayerItem)
            {
                value /= playerItemValueModifier;
            }
        }
        return value;
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
            return (data.itemName == i.data.itemName) && (id == i.id);
        }
    }

    public override int GetHashCode()
    {
        return data.itemName.GetHashCode();
    }
}
