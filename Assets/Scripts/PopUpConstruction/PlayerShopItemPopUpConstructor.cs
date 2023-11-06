using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Pop Up Constructor/Player Shop")]
public class PlayerShopItemPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        //Do something
        Debug.Log("Player Shop");
        popUpRef.AddText(itemRef.data.itemName + " x" + itemRef.amount);
    }
}
