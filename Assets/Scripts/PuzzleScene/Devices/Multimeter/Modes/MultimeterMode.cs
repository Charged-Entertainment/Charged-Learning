using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MultimeterMode : DeviceMode
{
    protected Multimeter multimeter;

    public virtual void OnEnter(Device device)
    {
        multimeter = (Multimeter)device;
        Debug.Log($"Entered ({this.GetType().Name}) Mode");
        //Subscribe to simulation results
    }

    public virtual void OnExit()
    {
        Debug.Log("Exited from ({this.GetType().Name}) Mode");
        //Unsubscribe from simulation results
    }

    ///<summary>Used to subscribe to SimulationResults events</summary>
    protected abstract void OnSimulationResults();
}