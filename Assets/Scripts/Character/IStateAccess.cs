public interface IStateAccess<T>
{
    void ChangeState(T newState);

    T GetCurrentState();
}
