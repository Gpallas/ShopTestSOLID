using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Player Shop")]
public class PlayerShopItemPopUpConstructor : BasePopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        popUpRef.AddText(itemRef.data.itemName + " x" + itemRef.amount);
    }

    protected override void BuildGoldEntry(IPopUpInfo popUpRef, Item itemRef)
    {
        popUpRef.AddGoldAmount(itemRef.GetItemValue(/*valueOfAllStack = */true));
    }
}
