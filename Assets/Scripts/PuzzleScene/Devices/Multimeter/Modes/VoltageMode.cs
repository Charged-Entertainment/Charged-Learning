using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoltageMode : DeviceMode
{
    bool ac;
    string measurementRange;

    /// <param name="ac">Decide weather you are measuring AC or DC voltage</param>
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public VoltageMode(bool ac, string measurementRange){
        this.ac = ac;
        this.measurementRange = measurementRange;
    }

    public void OnEnter()
    {
        Debug.Log($"Entered Voltage ({(ac?'A':'D')}) Mode");
        //Subscribe to simulation results
    }

    public void OnExit()
    {
        Debug.Log("Exited from Voltage Mode");
        //Unsubscribe from simulation results
    }

    //TODO: OnSimulationResults subscribe to results and display them
}