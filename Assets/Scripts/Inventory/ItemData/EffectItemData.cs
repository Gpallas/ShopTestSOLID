using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemData/Item With Effect")]
public class EffectItemData : ItemData
{
    public ValueType[] allEffects;

    public override string[] GetExtraData()
    {
        return base.GetExtraData();
    }

    public override object[] GetExtraDataForUI()
    {
        object[] data = new object[allEffects.Length];
        for (int i = 0; i < allEffects.Length; i++)
        {
            string effect = "";
            if (allEffects[i].value > 0)
            {
                effect = "+";
            }
            else if (allEffects[i].value < 0)
            {
                effect = "-";
            }
            effect += allEffects[i].value + " " + allEffects[i].typePurpose.ToUpper();
            data[i] = new EffectInfo(allEffects[i].icon, effect);
        }
        return data;
    }
}
