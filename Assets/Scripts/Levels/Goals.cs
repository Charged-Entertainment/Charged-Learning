using Components;
using UnityEngine;
using System.Collections.Generic;

public class ComponentMeasured : Goal
{
    public LevelComponent levelComponent;
    public ComponentMeasured(string message, LevelComponent component) : base(message)
    {
        this.levelComponent = component;
        LevelComponent.propertyRevealed += HandlePropertyRevealred;
    }

    private void HandlePropertyRevealred(LevelComponent component)
    {
        if (component == this.levelComponent)
        {
            Achieved = true;
            Puzzle.goalAchieved?.Invoke(this);
        }
    }
}


public class CircuitSubmit : Goal
{
    CircuitSubmit(string message) : base(message)
    {
        GameManagement.GameMode.changed += HandleModeChanged;
    }

    private void HandleModeChanged(State state)
    {
        if(state is GameManagement.Evaluate){
            Debug.Log("goal: game mode is evaluate");
            // CircuitBuilder.GetCircuitComponents();
            Achieved = true;
            Puzzle.goalAchieved?.Invoke(this);

        }
    }
}