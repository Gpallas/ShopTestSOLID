using System;

public interface ISelectAction
{
    void AddOnSelectAction(Action actionToTrigger);
}
