using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpiceSharp.Validation;
using Components;

public class SimulationManager : Singleton<SimulationManager>
{
    private static SpiceSharp.Simulations.IBiasingSimulation simulation;
    public static Action<SpiceSharp.Simulations.IBiasingSimulation> simulationDone;
    void Update()
    {

    }

    public static void Simulate()
    {
        //One node must be called "0" --represent the ground--
        Debug.Log("Starting simulation");
        simulation = new SpiceSharp.Simulations.Transient("sim1");

        var circuit = CircuitBuilder.Collect();

        // TEMP for testing. Get attach real models!!
        var testModel = new SpiceSharp.Components.DiodeModel("led_model"); 
        // testModel.SetParameter("Rs", 330.0);
        circuit.Add(testModel);

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
        simulationDone?.Invoke(simulation);

    }
}