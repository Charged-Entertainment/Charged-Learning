using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;


public class OffMode : MultimeterMode
{

    public override void OnEnter(Device device){
        base.OnEnter(device);
        multimeter.TurnOff();
    }

    public override void OnExit()
    {
        base.OnExit();
        multimeter.TurnOn();
    }
    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if(!multimeter.Connected) return;
        throw new System.NotImplementedException();
    }
}