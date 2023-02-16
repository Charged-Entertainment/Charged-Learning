using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpiceSharp.Validation;

public class SimulationManager : Singleton<SimulationManager>
{
    private static SpiceSharp.Simulations.IBiasingSimulation simulation;
    public static Action <SpiceSharp.Simulations.IBiasingSimulation> simulationDone;
    void Update()
    {
        
    }

    public static void Simulate()
    {
        //One node must be called "0" --represent the ground--
        Debug.Log("Starting simulation");
        simulation = new SpiceSharp.Simulations.Transient("sim1");

        var circuit = CircuitBuilder.Collect();
        simulation.ExportSimulationData += HandleSimulationResults;
        try
        {
            simulation.Run(circuit);
        }
        catch (SpiceSharp.Simulations.ValidationFailedException e)
        {
            Debug.Log("exception");
            FeebackTerminal.Write(new Log(RichText.Color(e.Message, PaletteColor.Red), LogType.Error));
            var violations = circuit.Validate();
            foreach (var violation in violations.Violations)
            {
                Debug.Log(violation.Rule);
                Debug.Log(violation.Subject);
            }
        }
    }

    private static void HandleSimulationResults(object sender, SpiceSharp.Simulations.ExportDataEventArgs args)
    {
        foreach (var c in GameObject.FindObjectsOfType<LiveComponent>())
        {
            double voltage = args.GetVoltage(CircuitBuilder.GetNode(c.Terminals[0]), CircuitBuilder.GetNode(c.Terminals[1]));
            Debug.Log($"Voltage across {c.Terminals[0].name} and {c.Terminals[1].name} on {c.gameObject.name} ({c.GetInstanceID()}) is {voltage}V");
        }

        simulationDone?.Invoke(simulation);

        // var multimeter = GameObject.FindObjectOfType<Multimeter>();
        // if(multimeter.DeviceMode is CurrentMode){
        //     var currentPropertyExport = new SpiceSharp.Simulations.RealCurrentExport(simulation, multimeter.GetInstanceID().ToString());
        //     Debug.Log($"Current across {multimeter.Terminals[0].name} and {multimeter.Terminals[1].name} on {multimeter.gameObject.name} ({multimeter.GetInstanceID()}) is {currentPropertyExport.Value}A");
        // }
        // double voltageMultimeter = args.GetVoltage(CircuitBuilder.GetNode(multimeter.Terminals[0]), CircuitBuilder.GetNode(multimeter.Terminals[1]));
        
        // Debug.Log($"Voltage across {multimeter.Terminals[0].name} and {multimeter.Terminals[1].name} on {multimeter.gameObject.name} ({multimeter.GetInstanceID()}) is {voltageMultimeter}V");

        FeebackTerminal.Write(new Log($"Voltage on the ground '0' node {RichText.Color(args.GetVoltage("0").ToString(), PaletteColor.Green)}"));
    }
}
