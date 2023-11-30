using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Player Shop")]
public class PlayerShopItemPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        //Do something
        Debug.Log("Player Shop");
        popUpRef.AddText(itemRef.data.itemName + " x" + itemRef.amount);
    }

    protected override void BuildGoldEntry(IPopUpInfo popUpRef, Item itemRef)
    {
        popUpRef.AddGoldAmount(itemRef.amount * itemRef.data.goldValue / 2);
    }
}
