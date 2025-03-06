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
    }

    ~FightStateMachine() { }

    public bool IsState(IFightState state)
    {
        return CurrentState == state;
    }

    public void OnUpdate()
    {
        CurrentState.OnUpdate();
    }


    public void ChangeTurn(IFightState state)
    {
        if (!CurrentState.TryChange(state)) return;
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
