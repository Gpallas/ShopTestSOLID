using System.Collections;
using UnityEngine;

public class TestPopUp : MonoBehaviour
{
    public PopUp popUpRef;
    public Item itemRef;
    public Item secondItemRef;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        itemRef.data.constructorRef.ConstructPopUp(popUpRef, itemRef);
        yield return new WaitForSeconds(3f);
        popUpRef.ClearPopUp();
        yield return new WaitForSeconds(1f);
        secondItemRef.data.constructorRef.ConstructPopUpWithGold(popUpRef, secondItemRef);
    }
}
