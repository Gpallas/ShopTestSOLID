using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopkeeperUIItem : UIItem
{
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemCost;
    [SerializeField]
    GameObject resellIcon;
    [SerializeField]
    TextMeshProUGUI itemAmount;

    protected override void PopulateItem()
    {
        image.sprite = item.data.image;
        itemName.text = item.data.itemName;
        itemCost.text = item.data.goldValue.ToString();
        if (item.data.wasSold)
        {
            resellIcon.SetActive(true);
            itemAmount.text = item.amount.ToString();
            itemAmount.gameObject.SetActive(true);
        }
    }
}
