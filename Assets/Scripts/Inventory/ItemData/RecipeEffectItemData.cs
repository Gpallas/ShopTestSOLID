using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemData/Recipe For Item With Effect")]
public class RecipeEffectItemData : ItemData
{
    public ValueType[] allIngredients;
    public ValueType[] allEffects;

    public override object[] GetExtraDataForUI()
    {
        object[] data = new object[allIngredients.Length + allEffects.Length];
        for (int i = 0; i < allIngredients.Length; i++)
        {
            data[i] = new IngredientInfo(allIngredients[i].icon, allIngredients[i].value, allIngredients[i].typePurpose);
        }
        for (int i = 0; i < allEffects.Length; i++)
        {
            string effect = "";
            if (allEffects[i].value > 0)
            {
                effect = "+";
            }
            else if (allEffects[i].value < 0)
            {
                effect = "-";
            }
            effect += allEffects[i].value + " " + allEffects[i].typePurpose.ToUpper();
            data[i + allIngredients.Length] = new EffectInfo(allEffects[i].icon, effect);
        }
        return data;
    }
}
