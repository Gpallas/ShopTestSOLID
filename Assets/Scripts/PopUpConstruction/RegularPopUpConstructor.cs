using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop Up Constructor/Regular")]
public class RegularPopUpConstructor : BasePopUpConstructor
{
    public override void ConstructPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        //Do something
        Debug.Log("Regular");
    }
}
