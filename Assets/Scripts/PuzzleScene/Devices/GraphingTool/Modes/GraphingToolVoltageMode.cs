using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;
using CLGraphing;

public class GraphingToolVoltageMode : GraphingToolMode
{
    bool ac;
    string measurementRange;

    /// <param name="ac">Decide weather you are measuring AC or DC voltage</param>
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public GraphingToolVoltageMode(bool ac, string measurementRange)
    {
        this.ac = ac;
        this.measurementRange = measurementRange;
    }


    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if (!graphingTool.InCircuit) return;
        // Debug.Log("Voltage mode sim handler is called");
        var voltageExport = new RealVoltageExport(simulation, CircuitBuilder.GetNode(graphingTool.Terminals[0]), CircuitBuilder.GetNode(graphingTool.Terminals[1]));
        GraphingTool.Plot(new Vector2(Graphing.time, -(float)voltageExport.Value));
    }
}