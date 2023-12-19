using UnityEngine;

public interface IPopUpInfo
{
    void AddTitle(string titleText, bool shouldResize = true);
    void AddText(string text, bool shouldResize = true);
    void AddGoldAmount(int goldAmount, bool shouldResize = true);
    void AddDivisory(bool shouldResize = true);
    void AddTextAndImage(string text, Sprite sprite, bool shouldResize = true);
    void AddCategory(ItemCategory category, bool shouldResize = true);
    void AddImageWithNumberAndText(Sprite sprite, int amount, string text, bool shouldResize = true);
    void FinishPopUp();

    void ClearPopUp();
}
