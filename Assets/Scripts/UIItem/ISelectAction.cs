using System;
using System.Collections;
using UnityEngine;

public interface ISelectAction
{
    void AddOnSelectAction(Action actionToTrigger);
}
