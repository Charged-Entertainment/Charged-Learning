using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    protected override void OnSimulationResults()
    {
        throw new System.NotImplementedException();
    }
}