using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public abstract class UIItemSubmitInputAction : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    protected Dictionary<string, Action<InputAction.CallbackContext>> actionsDictionary;
    protected Action onSelect;
    protected Action onDeselect;
    /*Ver a viabilidade de armazenar os InputActions ao invés de procurar toda vez que precisar usar
    protected Dictionary<string, InputAction> inputActionsDictionary;*/

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
        PlayerInput input = FindAnyObjectByType<PlayerInput>();

        foreach (KeyValuePair<string, Action<InputAction.CallbackContext>> action in actionsDictionary)
        {
            InputAction aux = input.actions.FindAction(action.Key);

            aux.started += actionsDictionary[action.Key];
            aux.performed += actionsDictionary[action.Key];
            aux.canceled += actionsDictionary[action.Key];

            if (aux.IsInProgress())
            {
                HandleInputActionAlreadyActive(aux.phase, action.Key);
            }
        }
        /*Deixar assim se for mais performático armazenar os InputActions ao inicializar ao invés de ter que procurar toda vez que precisar acessar
        foreach (KeyValuePair<string, InputAction> iterator in inputActionsDictionary)
        {
            inputActionsDictionary[iterator.Key].started += actionsDictionary[iterator.Key];
            inputActionsDictionary[iterator.Key].performed += actionsDictionary[iterator.Key];
            inputActionsDictionary[iterator.Key].canceled += actionsDictionary[iterator.Key];
        }*/
    }

    void UnsubscribeActions()
    {
        PlayerInput input = FindAnyObjectByType<PlayerInput>();

        if (input)
        {
            foreach (KeyValuePair<string, Action<InputAction.CallbackContext>> action in actionsDictionary)
            {
                InputAction aux = input.actions.FindAction(action.Key);

                aux.started -= actionsDictionary[action.Key];
                aux.performed -= actionsDictionary[action.Key];
                aux.canceled -= actionsDictionary[action.Key];
            }
        }
        /*Deixar assim se for mais performático armazenar os InputActions ao inicializar ao invés de ter que procurar toda vez que precisar acessar
        foreach (KeyValuePair<string, InputAction> iterator in inputActionsDictionary)
        {
            inputActionsDictionary[iterator.Key].started -= actionsDictionary[iterator.Key];
            inputActionsDictionary[iterator.Key].performed -= actionsDictionary[iterator.Key];
            inputActionsDictionary[iterator.Key].canceled -= actionsDictionary[iterator.Key];
        }*/
    }

    void OnDestroy()
    {
        UnsubscribeActions();
    }
}
