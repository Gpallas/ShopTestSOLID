using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerInput : PlayerInput
{
    public const string defaultMapName = "Player";
    public const string regularUIMapName = "UI";
    public const string myUIMapName = "MyUI";

    //Workaround for a strange bug when returning from an action map that's not the default nor UI
    public void SwitchActionMap(string mapName)
    {
        SwitchCurrentActionMap(mapName);
        actions.FindActionMap("UI").Disable();
        actions.FindActionMap("UI").Enable();
    }
}
