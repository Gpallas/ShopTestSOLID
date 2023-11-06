using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Recipe Item")]
public class RecipeItemData : ItemData
{
    public ValueType[] allIngredients;

    public override object[] GetExtraDataForUI()
    {
        object[] data = new object[allIngredients.Length];
        for (int i = 0; i < allIngredients.Length; i++)
        {
            data[i] = new IngredientInfo(allIngredients[i].icon, allIngredients[i].value, allIngredients[i].typePurpose);
        }
        return data;
    }
}
