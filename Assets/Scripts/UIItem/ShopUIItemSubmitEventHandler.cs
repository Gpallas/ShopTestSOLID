using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

//DEPRECATED. UIItemSubmitInputAction kept the OnSelect and OnDeselect functionalities and it's child classes implement the OnSubmit and press logic
public class ShopUIItemSubmitEventHandler : MonoBehaviour, ISubmitActionItem, ISelectHandler, IDeselectHandler, IUpdateItem
{
    Action<Item> onSubmitAction;
    Item item;

    InputAction test;
    InputAction test2;


    public void SubmitAllPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(context.control.name);
            Debug.Log(gameObject.name + " Submit All Pressed");
        }
    }

    public void SubmitSinglePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(context.control.name);
            Debug.Log(gameObject.name + " Submit Single Pressed");
        }
    }

    public void AddOnSubmitAction(Action<Item> actionTotrigger, Item itemToTrigger)
    {
        onSubmitAction += actionTotrigger;
        item = itemToTrigger;
    }

    public void OnSelect(BaseEventData eventData)
    {
        InputSystemUIInputModule inputModuleRef = (InputSystemUIInputModule)EventSystem.current.currentInputModule;

        PlayerInput qwe = FindAnyObjectByType<PlayerInput>();
        qwe.SwitchCurrentActionMap("MyUI");
        test = qwe.actions.FindAction("SubmitSingle");
        test2 = qwe.actions.FindAction("SubmitAll");

        test.started += SubmitSinglePressed;
        test.performed += SubmitSinglePressed;
        test.canceled += SubmitSinglePressed;

        test2.started += SubmitAllPressed;
        test2.performed += SubmitAllPressed;
        test2.canceled += SubmitAllPressed;

        /*
        inputModuleRef.submit.action.started += SubmitPressed;
        inputModuleRef.submit.action.performed += SubmitPressed;
        inputModuleRef.submit.action.canceled += SubmitPressed;*/
    }

    public void OnDeselect(BaseEventData eventData)
    {
        InputSystemUIInputModule inputModuleRef = (InputSystemUIInputModule)EventSystem.current.currentInputModule;
        
        test.started += SubmitAllPressed;
        test.performed += SubmitAllPressed;
        test.canceled += SubmitAllPressed;

        test2.started += SubmitAllPressed;
        test2.performed += SubmitAllPressed;
        test2.canceled += SubmitAllPressed;

        /*
        inputModuleRef.submit.action.started -= SubmitPressed;
        inputModuleRef.submit.action.performed -= SubmitPressed;
        inputModuleRef.submit.action.canceled -= SubmitPressed;*/
    }

    public void UpdateItem(Item newItem)
    {
        item = newItem;
    }
}
