
public class FiniteStateMachine
{
    public IBaseState CurrentState { get; private set; }

    public FiniteStateMachine()
    {
        CurrentState = null;
    }

    public FiniteStateMachine(IBaseState currentState)
    {
        CurrentState = currentState;
    }

    ~FiniteStateMachine() { }

    public bool IsState(IBaseState state)
    {
        return CurrentState == state;
    }

    public void OnUpdate()
    {
        CurrentState?.OnUpdate();
    }

    public void ChangeState(IBaseState state)
    {
        if(!IsState(state))
        {
            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState?.OnEnter();
        }
    }
}