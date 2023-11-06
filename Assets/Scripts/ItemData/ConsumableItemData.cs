using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Consumable Item")]
public class ConsumableItemData : ItemData
{
    [SerializeField]
    public Dictionary<string, int> extraData;

    public ValueType[] allExtraValues;

    public override string[] GetExtraData()
    {
        string[] data = new string[extraData.Count];
        int i = 0;
        foreach (KeyValuePair<string, int> pair in extraData)
        {
            data[i] = pair.Key + ":" + pair.Value.ToString();
            i++;
        }
        return data;
    }

    public override object[] GetExtraDataForUI()
    {
        object[] data = new object[allExtraValues.Length];
        for (int i = 0; i < allExtraValues.Length; i++)
        {
            string effect = "";
            if (allExtraValues[i].value > 0)
            {
                effect = "+";
            }
            else if (allExtraValues[i].value < 0)
            {
                effect = "-";
            }
            effect += allExtraValues[i].value + " " + allExtraValues[i].typePurpose.ToUpper();
            data[i] = new EffectInfo(allExtraValues[i].icon, effect);
        }
        return data;
    }
}
