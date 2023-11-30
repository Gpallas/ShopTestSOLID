using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemCategory")]
public class ItemCategory : ScriptableObject
{
    public string categoryName;
    public Color categoryColor;
}
