using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop Up Constructor/Player Shop")]
public class PlayerShopItemPopUpConstructor : BasePopUpConstructor
{
    public override void ConstructPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        //Do something
        Debug.Log("Player Shop");
    }
}
