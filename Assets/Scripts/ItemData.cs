using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class ItemData : ScriptableObject
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

    public ItemCategory category;

    public bool stackable = true;
    public int stackLimit;

    public BasePopUpConstructor constructorRef;

    public override bool Equals(object other)
    {
        //Check for null and compare run-time types.
        if ((other == null) || !this.GetType().Equals(other.GetType()))
        {
            return false;
        }
        else
        {
            ItemData i = (ItemData)other;
            return (itemName == i.itemName);
        }
    }

    public override int GetHashCode()
    {
        return itemName.GetHashCode();
    }
}
