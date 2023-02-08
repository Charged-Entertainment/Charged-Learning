using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoltageMode : MultimeterMode
{
    bool ac;
    string measurementRange;

    /// <param name="ac">Decide weather you are measuring AC or DC voltage</param>
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public VoltageMode(bool ac, string measurementRange){
        this.ac = ac;
        this.measurementRange = measurementRange;
    }


    protected override void OnSimulationResults()
    {
        //something
        //then terminal.DisplayMeasurement(some value)
        throw new System.NotImplementedException();
    }
}