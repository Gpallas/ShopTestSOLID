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
        if (item != null)
        {
            image.sprite = item.data.image;
            image.gameObject.SetActive(true);

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
        else
        {
            image.gameObject.SetActive(false);
            amount.gameObject.SetActive(false);
        }
    }
}