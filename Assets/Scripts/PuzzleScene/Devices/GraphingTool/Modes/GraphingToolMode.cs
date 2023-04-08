using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GraphingToolMode : DeviceMode
{
    protected GraphingTool graphingTool;

    public virtual void OnEnter(Device device)
    {
        graphingTool = (GraphingTool)device;
        //Subscribe to simulation results
        SimulationManager.simulationDone += HandleSimulationDone;
    }

    public virtual void OnExit()
    {
        //Unsubscribe from simulation results
        SimulationManager.simulationDone -= HandleSimulationDone;

    }

    ///<summary>Used to subscribe to SimulationResults events</summary>
    protected abstract void HandleSimulationDone(SpiceSharp.Simulations.IBiasingSimulation simulation);
}