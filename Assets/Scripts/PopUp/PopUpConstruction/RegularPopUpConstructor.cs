using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Regular")]
public class RegularPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        popUpRef.AddTitle(itemRef.data.itemName);
        popUpRef.AddCategory(itemRef.data.category);
        popUpRef.AddDivisory();
        popUpRef.AddText(itemRef.data.description, /*shouldResize = */false);
    }
}
