public interface IGoldAccess
{
    void AddGold(int amount);

    void RemoveGold(int amount);

    bool CheckHasEnoughGold(int minimumValue);

    int GetCurrentGold();
}