using Components;
using UnityEngine;
using System.Collections.Generic;

public class ComponentValueMeasuredGoal : Goal
{
    public string componentSpiceName {get; private set;}
    public string property {get; private set;}
    public double neededValue {get; private set;}
    public ComponentValueMeasuredGoal(string message, string componentSpiceName, string property, double neededValue) : base(message)
    {
        this.componentSpiceName = componentSpiceName;
        this.property = property;
        this.neededValue = neededValue;
        SimulationManager.simulationDone += HandleSimulationDone;
    }

    public ComponentValueMeasuredGoal(string message, LiveComponent component, string property, double neededValue) : base(message)
    {
        this.componentSpiceName = component.ID();
        this.property = property;
        this.neededValue = neededValue;
        SimulationManager.simulationDone += HandleSimulationDone;
    }

    private void HandleSimulationDone(SpiceSharp.Simulations.IBiasingSimulation simulation)
    {
        var val = new SpiceSharp.Simulations.RealPropertyExport(simulation, componentSpiceName, property).Value;
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