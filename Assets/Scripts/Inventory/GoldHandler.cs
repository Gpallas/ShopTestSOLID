using System.Collections;
using UnityEngine;

public class GoldHandler : MonoBehaviour, IGoldAccess
{
    int currentGold;
    int maximumAmount = 999999999;

    void Start()
    {
        //This should be called on a load from somewhere
        LoadGoldAmount();
    }

    void LoadGoldAmount()
    {
        // Gets value from some save file
        currentGold = 0;
    }

    public void AddGold(int amount)
    {
        currentGold = (currentGold + amount > maximumAmount) ? maximumAmount : currentGold + amount;
    }

    public void RemoveGold(int amount)
    {
        currentGold = (amount > currentGold) ? 0 : currentGold - amount;
    }

    public bool CheckHasEnoughGold(int minimumValue)
    {
        return currentGold >= minimumValue;
    }

    public int GetCurrentGold()
    {
        return currentGold;
    }
}
