using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour, IPopUpInfo
{
    public void ClearPopUp()
    {
        Debug.Log("Clear PopUp");
    }

    public void ShowInfo(string name, string category, string description, string gold)
    {
        throw new System.NotImplementedException();
    }

    public void ShowInfo(string name, int amount, string gold)
    {
        throw new System.NotImplementedException();
    }

    public void ShowInfo(Item itemToShow)
    {
        throw new System.NotImplementedException();
    }
}
