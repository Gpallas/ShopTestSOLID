using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegularUIItem : UIItem
{
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI amount;

    protected override void PopulateItem()
    {
        image.sprite = item.data.image;
        if (item.amount > 1)
        {
            amount.text = item.amount.ToString();
            amount.gameObject.SetActive(true);
        }
        else
        {
            amount.gameObject.SetActive(false);
        }
    }
}