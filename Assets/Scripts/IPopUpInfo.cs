using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IPopUpInfo
{
    void ShowInfo(string name, string category, string description, string gold);
    void ShowInfo(string name, int amount, string gold);
    void ShowInfo(Item itemToShow);

    void AddTitle(string titleText);
    void AddText(string text);
    void AddGoldAmount(int goldAmount);
    void AddDivisory();
    void AddTextAndImage(string text, Sprite sprite);
    void AddCategory(ItemData.ItemCategory category);
    void AddImageWithNumberAndText(Sprite sprite, int amount, string text);
    void AdjustSize();
    void ChangeIndentationLevel(int amountTochange);

    void ClearPopUp();
}
