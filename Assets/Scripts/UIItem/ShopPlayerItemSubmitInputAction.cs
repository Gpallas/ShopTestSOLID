using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopPlayerItemSubmitInputAction : UIItemSubmitInputAction, ISubmitActionItem, IUpdateItem
{
    const string SingleSubmit = "SubmitSingle";
    const string MultipleSubmit = "SubmitMultipleSingle";
    const string AllSubmit = "SubmitAll";
    const string MultipleAllSubmit = "SubmitMultipleAll";
    const float WaitTime = 1f;
    const float InitialWaitTime = 0.5f;

    Action<Item> onSubmitAction;
    Item item;

    Dictionary<string, Coroutine> allCoroutines;

    protected override void Initialize()
    {
        PlayerInput inputComponent = FindAnyObjectByType<PlayerInput>();
        inputActionDictionary = new Dictionary<InputAction, Action<InputAction.CallbackContext>>
        {
            { inputComponent.actions[SingleSubmit], OnSubmitSingle },
            { inputComponent.actions[MultipleSubmit], OnSubmitMultiple },
            { inputComponent.actions[AllSubmit], OnSubmitAll },
            { inputComponent.actions[MultipleAllSubmit], OnSubmitAll }
        };

        onDeselect += StopAll;
        allCoroutines = new Dictionary<string, Coroutine>();
    }

    int SingleItemAmount()
    {
        return 1;
    }

    int MultipleItemAmount()
    {
        return  (item != null) ? Mathf.CeilToInt((float)item.amount / 2f) : -1;
    }

    int AllItemAmount()
    {
        return (item != null) ? item.data.stackLimit : -1;
    }

    protected override void HandleInputActionAlreadyActive(InputActionPhase currentPhase, string inputActionName)
    {
        Func<int> GetAmount;

        if (inputActionName == SingleSubmit)
        {
            GetAmount = SingleItemAmount;
        }
        else if (inputActionName == MultipleSubmit)
        {
            GetAmount = MultipleItemAmount;
        }
        else if (inputActionName == AllSubmit || inputActionName == MultipleAllSubmit)
        {
            return;
        }
        else
        {
            Debug.Log("InputAction name does not exist in names list");
            return;
        }

        if (currentPhase == InputActionPhase.Started)
        {
            SubmitStart(GetAmount());
        }
        else if (currentPhase == InputActionPhase.Performed)
        {
            SubmitPerformed(GetAmount, inputActionName);
        }
    }

    void OnSubmitSingle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SubmitStart(SingleItemAmount());
        }
        else if (context.performed)
        {
            SubmitPerformed(SingleItemAmount, context.action.name);
        }
        else if (context.canceled)
        {
            SubmitCancelled(context.action.name);
        }
    }

    void OnSubmitMultiple(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SubmitStart(MultipleItemAmount());
        }
        else if (context.performed)
        {
            SubmitPerformed(MultipleItemAmount, context.action.name);
        }
        else if (context.canceled)
        {
            SubmitCancelled(context.action.name);
        }
    }

    void OnSubmitAll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SubmitStart(AllItemAmount());
        }
    }

    void SubmitStart(int amount)
    {
        if (item != null)
        {
            item.amount = amount;
            onSubmitAction?.Invoke(item);
        }
    }

    void SubmitPerformed(Func<int> GetAmount, string inputName)
    {
        if (item != null)
        {
            if (!allCoroutines.ContainsKey(inputName))
            {
                allCoroutines.Add(inputName, StartCoroutine(SubmitLoop(GetAmount)));
            }
        }
    }

    void SubmitCancelled(string inputName)
    {
        if (item != null)
        {
            if (allCoroutines[inputName] != null)
            {
                StopCoroutine(allCoroutines[inputName]);
                allCoroutines.Remove(inputName);
            }
        }
    }

    public void AddOnSubmitAction(Action<Item> actionTotrigger, Item itemToTrigger)
    {
        onSubmitAction += actionTotrigger;
        item = itemToTrigger;
    }

    IEnumerator SubmitLoop(Func<int> GetAmount)
    {
        yield return new WaitForSeconds(InitialWaitTime);
        while(true)
        {
            item.amount = GetAmount();
            onSubmitAction?.Invoke(item);
            yield return new WaitForSeconds(WaitTime);
        }
    }

    void StopAll()
    {
        foreach (KeyValuePair<string, Coroutine> pair in allCoroutines)
        {
            StopCoroutine(pair.Value);
        }
        allCoroutines.Clear();
    }

    public void UpdateItem(Item newItem)
    {
        item = newItem;
    }
}
