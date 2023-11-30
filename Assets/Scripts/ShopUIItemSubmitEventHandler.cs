using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

//DEPRECATED. UIItemSubmitInputAction ficou com a base dessa funcionalidade do OnSelect e tals e ShopPlayerItemSubmitInputAction ficou com a funcionalidade de fato de quando os InputActions são chamados
public class ShopUIItemSubmitEventHandler : MonoBehaviour, ISubmitActionItem, ISelectHandler, IDeselectHandler, IUpdateItem
{
    Action<Item> onSubmitAction;
    Item item;

    InputAction test;
    InputAction test2;

    //Preciso fazer com que esses dois métodos sejam adicionados e removidos dinamicamente do PlayerInput, já que o InpuSystemUIInputModule tem uma limitação de não poder adicionar Actions extras

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
