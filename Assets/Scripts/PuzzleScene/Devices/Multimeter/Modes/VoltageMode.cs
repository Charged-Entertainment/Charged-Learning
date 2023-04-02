using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Simulations;

public class VoltageMode : MultimeterMode
{
    bool ac;
    string measurementRange;


    public override void OnEnter(Device device)
    {
        base.OnEnter(device);
        multimeter.DisplayMessage("0V");

    }

    /// <param name="ac">Decide weather you are measuring AC or DC voltage</param>
    /// <param name="measurementRange">The range you expect the measurement to be in</param>
    public VoltageMode(bool ac, string measurementRange)
    {
        this.ac = ac;
        this.measurementRange = measurementRange;
    }


    protected override void HandleSimulationDone(IBiasingSimulation simulation)
    {
        if (!multimeter.InCircuit) return;
        // Debug.Log("Voltage mode sim handler is called");
        var voltageExport = new RealVoltageExport(simulation, CircuitBuilder.GetNode(multimeter.Terminals[0]), CircuitBuilder.GetNode(multimeter.Terminals[1]));
        multimeter.DisplayMessage($"{voltageExport.Value}V");
    }
}