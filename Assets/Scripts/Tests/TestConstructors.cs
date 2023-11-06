using System.Collections;
using UnityEngine;

public class TestConstructors : MonoBehaviour
{
    public Item testItem;
    public PopUp popUpRef;

    void Start()
    {
        testItem.data.constructorRef.ConstructPopUp(popUpRef, testItem);
    }
}
