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
        throw new System.NotImplementedException();
    }
}