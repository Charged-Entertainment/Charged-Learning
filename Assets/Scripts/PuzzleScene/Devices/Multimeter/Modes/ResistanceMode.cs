using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResistanceMode : DeviceMode
{
    string measurementRange;

    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public ResistanceMode(string measurementRange){
        this.measurementRange = measurementRange;
    }

    public void OnEnter()
    {
        Debug.Log("Entered Resistance Mode");
        //Subscribe to simulation results
    }

    public void OnExit()
    {
        Debug.Log("Exited from Resistance Mode");
        //Unsubscribe from simulation results
    }

    //TODO: OnSimulationResults subscribe to results and display them
}