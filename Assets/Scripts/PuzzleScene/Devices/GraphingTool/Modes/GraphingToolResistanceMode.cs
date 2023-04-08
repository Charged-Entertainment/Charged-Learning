using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;
using CLGraphing;


public class GraphingToolResistanceMode : GraphingToolMode
{
    string measurementRange;
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public GraphingToolResistanceMode(string measurementRange)
    {
        this.measurementRange = measurementRange;
    }

    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if (!graphingTool.InCircuit) return;
        var currentExport = new RealCurrentExport(simulation, graphingTool.ID());
        //TODO: magic number set create a constant for it
        double val = System.Math.Abs(1 / currentExport.Value);
        GraphingTool.Plot(new Vector2(Graphing.time, (float)val));
    }
}