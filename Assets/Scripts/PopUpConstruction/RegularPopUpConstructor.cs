using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Regular")]
public class RegularPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        //Do something
        Debug.Log("Regular");
        popUpRef.AddTitle(itemRef.data.itemName);
        popUpRef.AddCategory(itemRef.data.category);
        popUpRef.AddDivisory();
        popUpRef.AddText(itemRef.data.description, /*shouldResize = */false);
    }
}
