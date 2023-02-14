using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State
{
    protected StateMachine<State> _context;

    public void SetContext(StateMachine<State> context)
    {
        this._context = context;
    }
}

abstract public class StateMachine<State> : Singleton<StateMachine<State>>
{
    public static Action<State> changed;
    public static State Current {get; private set;}
    public static void ChangeTo(State state)
    {
        Current = state;
        // CurrentState.SetContext(this);
        
        changed?.Invoke(state);
        Debug.Log($"Context: Transition to {state.GetType().Name}.");
    }
    public static implicit operator State(StateMachine<State> s) {return Current;}
}
