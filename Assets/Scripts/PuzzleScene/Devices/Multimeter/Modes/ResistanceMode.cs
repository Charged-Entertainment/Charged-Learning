using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;

public class ResistanceMode : MultimeterMode
{
    string measurementRange;

    public override void OnEnter(Device device)
    {
        base.OnEnter(device);
        multimeter.DisplayMessage("0Ω");
    }

    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public ResistanceMode(string measurementRange){
        this.measurementRange = measurementRange;
    }

    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if (!multimeter.InCircuit) return;
        var currentExport = new RealCurrentExport(simulation, multimeter.ID());
        //TODO: magic number set create a constant for it
        double val = System.Math.Abs(1/currentExport.Value);
        multimeter.DisplayMessage($"{val}Ω");
        Multimeter.measured?.Invoke(val, Components.PropertyType.Resistance);
    }
}