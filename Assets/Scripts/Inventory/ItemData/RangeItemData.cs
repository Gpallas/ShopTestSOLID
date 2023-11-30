using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemData/Item With Range")]
public class RangeItemData : ItemData
{
    public RangeType[] allRanges;

    public override string[] GetExtraData()
    {
        return base.GetExtraData();
    }

    public override object[] GetExtraDataForUI()
    {
        object[] data = new object[allRanges.Length];
        for (int i = 0; i < allRanges.Length; i++)
        {
            string effect = allRanges[i].range.x + " - " + allRanges[i].range.y + " " + allRanges[i].typePurpose.ToUpper();
            data[i] = new EffectInfo(allRanges[i].icon, effect);
        }
        return data;
    }
}
