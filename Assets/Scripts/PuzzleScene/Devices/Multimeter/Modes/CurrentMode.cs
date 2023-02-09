using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentMode : MultimeterMode
{
    bool ac;
    string measurementRange;

    /// <param name="ac">Decide weather you are measuring AC or DC voltage</param>
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public CurrentMode(bool ac, string measurementRange){
        this.ac = ac;
        this.measurementRange = measurementRange;
    }


    protected override void OnSimulationResults()
    {
        throw new System.NotImplementedException();
    }
}