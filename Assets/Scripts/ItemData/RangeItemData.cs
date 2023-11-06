using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Range Item")]
public class RangeItemData : ItemData
{
    public Dictionary<string, RangeInt> extraData;

    public RangeType[] allRanges;

    public override string[] GetExtraData()
    {
        string[] data = new string[extraData.Count];
        int i = 0;
        foreach (KeyValuePair<string, RangeInt> pair in extraData)
        {
            data[i] = pair.Key + ":" + pair.Value.ToString();
            i++;
        }
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
