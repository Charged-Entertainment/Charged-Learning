using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;

public class LockedMode : MultimeterMode
{
    public override void OnEnter(Device device)
    {
        base.OnEnter(device);
        multimeter.DisplayMessage("Locked");
    }
    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        Debug.Log("Multimeter recieved simulation result but current mode is not unlocked");
    }
}