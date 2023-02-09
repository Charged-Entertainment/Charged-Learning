using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResistanceMode : MultimeterMode
{
    string measurementRange;

    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public ResistanceMode(string measurementRange){
        this.measurementRange = measurementRange;
    }

    protected override void OnSimulationResults()
    {
        throw new System.NotImplementedException();
    }
}