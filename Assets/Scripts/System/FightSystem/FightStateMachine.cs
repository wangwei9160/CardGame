using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStateMachine
{
    public IFightState CurrentState { get; private set; }

    public FightStateMachine(IFightState currentState)
    {
        CurrentState = currentState;
        currentState.OnEnter();
        EventCenter.AddListener<IFightState>(EventDefine.ChangeState, ChangeState);
    }

    ~FightStateMachine()
    {
        EventCenter.RemoveListener<IFightState>(EventDefine.ChangeState, ChangeState);
    }

    public bool IsState(IFightState state)
    {
        return CurrentState == state;
    }

    public void OnUpdate()
    {
        CurrentState.OnUpdate();
    }

    public void ChangeState(IFightState state)
    {
        if (!CurrentState.TryChange(state)) return;
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
