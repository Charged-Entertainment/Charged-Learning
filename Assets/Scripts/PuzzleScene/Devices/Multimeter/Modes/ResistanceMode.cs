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
        var currentExport = new RealCurrentExport(simulation, multimeter.gameObject.GetInstanceID().ToString());
        //TODO: magic number set create a constant for it
        multimeter.DisplayMessage($"{System.Math.Abs(1/currentExport.Value)}Ω");
        multimeter.ConnectedComponent.levelComponent.RevealProperty(Components.PropertyType.Resistance);
    }
}