using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PopUpConstructor/Item With Effect")]
public class EffectItemPopUpConstructor : RegularPopUpConstructor
{
    protected override void BuildPopUp(IPopUpInfo popUpRef, Item itemRef)
    {
        base.BuildPopUp(popUpRef, itemRef);

        foreach (EffectInfo info in itemRef.data.GetExtraDataForUI())
        {
            popUpRef.AddTextAndImage(info.effect, info.icon);
        }
    }
}
