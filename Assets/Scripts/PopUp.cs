using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour, IPopUpInfo
{
    public void AddCategory(ItemData.ItemCategory category)
    {
        
    }

    public void AddDivisory()
    {
        
    }

    public void AddGoldAmount(int goldAmount)
    {
        
    }

    public void AddImageWithNumberAndText(Sprite sprite, int amount, string text)
    {
        
    }

    public void AddText(string text)
    {
        
    }

    public void AddTextAndImage(string text, Sprite sprite)
    {
        
    }

    public void AddTitle(string titleText)
    {
        
    }

    public void AdjustSize()
    {
        
    }

    public void ChangeIndentationLevel(int amountTochange)
    {
        
    }

    public void ClearPopUp()
    {
        Debug.Log("Clear PopUp");
    }

    public void ShowInfo(string name, string category, string description, string gold)
    {
        
    }

    public void ShowInfo(string name, int amount, string gold)
    {
        
    }

    public void ShowInfo(Item itemToShow)
    {
        
    }
}
