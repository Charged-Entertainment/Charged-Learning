using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;


public class GraphingToolOffMode : GraphingToolMode
{

    public override void OnEnter(Device device){
        // graphingTool.TurnOff();
    }

    public override void OnExit()
    {
        // base.OnExit();
        // graphingTool.TurnOn();
    }
    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if(!graphingTool.InCircuit) return;
        throw new System.NotImplementedException();
    }
}