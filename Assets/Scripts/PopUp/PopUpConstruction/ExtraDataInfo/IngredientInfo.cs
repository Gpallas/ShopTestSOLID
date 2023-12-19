using UnityEngine;

public struct IngredientInfo
{
    public IngredientInfo(Sprite newIcon, int newAmount, string newName)
    {
        icon = newIcon;
        amount = newAmount;
        ingredientName = newName;
    }

    public Sprite icon;
    public int amount;
    public string ingredientName;
}
