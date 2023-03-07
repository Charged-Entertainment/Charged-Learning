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

public class ComponentValueMeasuredGoal : Goal
{
    string componentSpiceName;
    string property;
    double neededValue;
    public ComponentValueMeasuredGoal(string message, string componentSpiceName, string property, double neededValue) : base(message)
    {
        this.componentSpiceName = componentSpiceName;
        this.property = property;
        this.neededValue = neededValue;
        SimulationManager.simulationDone += HandleSimulationDone;
    }

    public ComponentValueMeasuredGoal(string message, LiveComponent component, string property, double neededValue) : base(message)
    {
        this.componentSpiceName = component.gameObject.GetInstanceID().ToString();
        this.property = property;
        this.neededValue = neededValue;
        SimulationManager.simulationDone += HandleSimulationDone;
    }

    private void HandleSimulationDone(SpiceSharp.Simulations.IBiasingSimulation simulation)
    {
        var val = new SpiceSharp.Simulations.RealPropertyExport(simulation, componentSpiceName, "i").Value;
        if (Utils.Approximately(neededValue, val))
        {
            Achieved = true;
            SimulationManager.simulationDone -= HandleSimulationDone;
            Puzzle.goalAchieved?.Invoke(this);
        }
    }
}


public class CircuitSubmit : Goal
{
    private LevelComponent battery;
    private LevelComponent resistor;
    public CircuitSubmit(string message, LevelComponent battery, LevelComponent resistor) : base(message)
    {
        SimulationManager.simulationDone += HandleModeChanged;
        this.battery = battery;
        this.resistor = resistor;
    }

    private void HandleModeChanged(SpiceSharp.Simulations.IBiasingSimulation simulation)
    {
        if (GameManagement.GameMode.Current is GameManagement.Evaluate)
        {
            Debug.Log("goal: game mode is evaluate");
            var circuitComponents = CircuitBuilder.GetCircuitComponents();
            var circuitLevelComponent = new HashSet<LevelComponent>();
            foreach (var component in circuitComponents)
            {
                var liveComponent = (component as LiveComponent);
                if (liveComponent != null)
                {
                    circuitLevelComponent.Add(liveComponent.levelComponent);
                }
            }

            if (circuitLevelComponent.Contains(battery) && circuitLevelComponent.Contains(resistor))
            {
                Achieved = true;
                Puzzle.goalAchieved?.Invoke(this);
            }

        }
    }
}