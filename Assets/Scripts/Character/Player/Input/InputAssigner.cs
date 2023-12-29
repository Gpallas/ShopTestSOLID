using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputAssigner : MonoBehaviour
{
    [SerializeField]
    MyPlayerInput inputComponent;

    public static event Action<PlayerInput> assignCaller;
    public static event Action unassignCaller;

    void OnEnable()
    {
        assignCaller?.Invoke(inputComponent);
    }

    void OnDisable()
    {
        unassignCaller?.Invoke();
    }

    void OnDestroy ()
    {
        unassignCaller?.Invoke();
    }
}
