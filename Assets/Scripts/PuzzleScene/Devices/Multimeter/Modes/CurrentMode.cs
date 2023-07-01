using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;

public class CurrentMode : MultimeterMode
{
    bool ac;
    string measurementRange;

    public override void OnEnter(Device device)
    {
        base.OnEnter(device);
        multimeter.DisplayMessage("0A");
    }

    /// <param name="ac">Decide weather you are measuring AC or DC voltage</param>
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public CurrentMode(bool ac, string measurementRange)
    {
        this.ac = ac;
        this.measurementRange = measurementRange;
    }


    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if (!multimeter.InCircuit) return;
        var currentExport = new RealCurrentExport(simulation, multimeter.ID());
        multimeter.DisplayMessage($"{currentExport.Value}A");
        
    }
}