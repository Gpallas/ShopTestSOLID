using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public Item item;

    List<Item> itemList;

    public List<Item> itemClassList;

    // Start is called before the first frame update
    void Start()
    {

        //GetComponent<ShopPlayerItemSubmitInputAction>().Test2(itemClassList[0]);//new Item(itemClassList[0]));

        item.amount = 547;
        //StartCoroutine(PrintAmount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PrintAmount()
    {
        while(true)
        {
            for (int i=0; i<itemClassList.Count; i++)
            {
                Debug.Log(itemClassList[i].amount);
            }
            Debug.Log("Break");
            yield return new WaitForSeconds(1);
        }
    }

    public void PrintSelect()
    {
        Debug.Log("Select");
    }

    public void PrintDeselect()
    {
        Debug.Log("Deselect");
    }

    public void PrintEnter()
    {
        Debug.Log("Pointer enter");
    }

    public void PrintExit()
    {
        Debug.Log("Pointer exit");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void OnSubmit(InputValue value)//InputInteractionContext interaction, InputAction.CallbackContext context)
    {
        //Debug.Log(gameObject.name + " Received Message");
    }
}
