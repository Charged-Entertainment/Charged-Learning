using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpiceSharp.Validation;
using SpiceSharp.Simulations.IntegrationMethods;
using Components;
using GameManagement;

public class SimulationManager : Singleton<SimulationManager>
{
    private static SpiceSharp.Simulations.IBiasingSimulation simulation;
    public static Action<SpiceSharp.Simulations.IBiasingSimulation> simulationDone;
    private static double startTime = 0;
    private static bool ongoingSimulation = false;
    private static double elapsed = 0;
    private static double steps = 0.1;

    private new void Awake()
    {
        base.Awake();
        GameMode.changed += HandleGameModeChanged;
    }

    void Update()
    {
        if (ongoingSimulation)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= steps)
            {
                elapsed = elapsed % steps;
                startTime++;
                Simulate();
            }
        }
    }

    private static void Simulate()
    {
        //One node must be called "0" --represent the ground--
        Debug.Log("Starting simulation");
        var timeParameters = new FixedEuler() { StartTime = startTime, StopTime = startTime, Step = 1 };
        simulation = new SpiceSharp.Simulations.Transient("sim1", timeParameters);

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
    void HandleGameModeChanged(State state)
    {
        if (state == GameModes.Edit)
        {
            ongoingSimulation = false;
            startTime = 0;
            return;
        }

        ongoingSimulation = true;

    }

    private static void HandleSimulationResults(object sender, SpiceSharp.Simulations.ExportDataEventArgs args)
    {
        simulationDone?.Invoke(simulation);

    }
}