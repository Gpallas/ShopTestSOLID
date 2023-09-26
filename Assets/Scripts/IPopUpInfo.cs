using System.Collections;
using UnityEngine;

public interface IPopUpInfo
{
    void ShowInfo(string name, string category, string description, string gold);
    void ShowInfo(string name, int amount, string gold);

    void ClearPopUp();
}
