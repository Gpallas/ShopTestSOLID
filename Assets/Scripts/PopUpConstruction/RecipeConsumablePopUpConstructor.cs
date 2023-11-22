using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Recipe For Consumable")]
public class RecipeConsumablePopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {

        popUpRef.AddTitle(itemRef.data.itemName);
        popUpRef.AddCategory(itemRef.data.category);
        popUpRef.AddDivisory();

        popUpRef.AddText("Ingredients");
        object[] data = itemRef.data.GetExtraDataForUI();
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] is IngredientInfo)
            {
                IngredientInfo info = (IngredientInfo)data[i];
                popUpRef.AddImageWithNumberAndText(info.icon, info.amount, info.ingredientName);
            }
        }

        popUpRef.AddText(itemRef.data.description, /*shouldResize = */false);

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] is EffectInfo)
            {
                EffectInfo info = (EffectInfo)data[i];
                popUpRef.AddTextAndImage(info.effect, info.icon);
            }
        }
    }
}