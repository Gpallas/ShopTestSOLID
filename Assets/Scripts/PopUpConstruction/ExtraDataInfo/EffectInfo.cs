using System.Collections;
using UnityEngine;

public struct EffectInfo
{
    public EffectInfo(Sprite newIcon, string newEffect)
    {
        icon = newIcon;
        effect = newEffect;
    }

    public Sprite icon;
    public string effect;
}
