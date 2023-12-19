using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Recipe")]
public class RecipeItemPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {

        popUpRef.AddTitle(itemRef.data.itemName);
        popUpRef.AddCategory(itemRef.data.category);
        popUpRef.AddDivisory();

        popUpRef.AddText("Ingredients");

        foreach (IngredientInfo i in itemRef.data.GetExtraDataForUI())
        {
            popUpRef.AddImageWithNumberAndText(i.icon, i.amount, i.ingredientName);
        }

        popUpRef.AddText(itemRef.data.description, /*shouldResize = */false);
    }
}
