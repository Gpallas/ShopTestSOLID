using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop Up Constructor/Recipe")]
public class RecipeItemPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {

        popUpRef.AddTitle(itemRef.data.itemName);
        popUpRef.AddCategory(itemRef.data.category);
        popUpRef.AddDivisory();

        popUpRef.AddText("Ingredients");

        popUpRef.ChangeIndentationLevel(/*amountToChange = */1);
        foreach (IngredientInfo i in itemRef.data.GetExtraDataForUI())
        {
            popUpRef.AddImageWithNumberAndText(i.icon, i.amount, i.ingredientName);
        }
        popUpRef.ChangeIndentationLevel(/*amountToChange = */-1);

        popUpRef.AddText(itemRef.data.description);
    }
}
