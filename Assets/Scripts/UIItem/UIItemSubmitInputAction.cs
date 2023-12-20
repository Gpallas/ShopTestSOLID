using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86;

public abstract class UIItemSubmitInputAction : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    protected Action onSelect;
    protected Action onDeselect;
    protected Dictionary<InputAction, Action<InputAction.CallbackContext>> inputActionDictionary;

    void Awake()
    {
        Initialize();
    }

    protected abstract void Initialize();
    protected abstract void HandleInputActionAlreadyActive(InputActionPhase currentPhase, string inputActionName);

    public void OnSelect(BaseEventData eventData)
    {
        SubscribeActions();

        onSelect?.Invoke();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        UnsubscribeActions();

        onDeselect?.Invoke();
    }

    void SubscribeActions()
    {
        foreach (KeyValuePair<InputAction, Action<InputAction.CallbackContext>> pair in inputActionDictionary)
        {
            pair.Key.started += pair.Value;
            pair.Key.performed += pair.Value;
            pair.Key.canceled += pair.Value;

            if (pair.Key.IsInProgress())
            {
                HandleInputActionAlreadyActive(pair.Key.phase, pair.Key.name);
            }
        }
    }

    void UnsubscribeActions()
    {
        foreach (KeyValuePair<InputAction, Action<InputAction.CallbackContext>> pair in inputActionDictionary)
        {
            pair.Key.started -= pair.Value;
            pair.Key.performed -= pair.Value;
            pair.Key.canceled -= pair.Value;
        }
    }

    void OnDestroy()
    {
        UnsubscribeActions();
    }
}
