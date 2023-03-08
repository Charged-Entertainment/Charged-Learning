using UnityEngine;
using SpiceSharp;
using SpiceSharp.Simulations;
using GameManagement;

public class LedCurrentBehvior : MonoBehaviour
{
    private readonly static float CURRENT_TO_INTENSITY_MULT = 0.0375f;
    private readonly static double MIN_CURRENT = 10e-3;
    private readonly static double OPTIMAL_CURRENT = 20e-3;
    private readonly static double ACCEPTABLE_CURRENT_DIFF = 2.5e-3;
    private readonly static double MAX_CURRENT = 30e-3;

    /// <summary>
    /// Non-linear (quadratic) conversion from current in milliamps to LED intensity.
    /// </summary>
    public static float CurrentToIntensity(float current_mA)
    {
        return Mathf.Pow(current_mA, 2) * CURRENT_TO_INTENSITY_MULT;
    }

    Led led;
    private void Awake()
    {
        led = gameObject.GetComponent<Led>();
    }

    private void OnEnable()
    {
        SimulationManager.simulationDone += HandleCurrentChanged;
        GameMode.changed += HandleGameModeChanged;
    }

    private void OnDisable()
    {
        SimulationManager.simulationDone -= HandleCurrentChanged;
        GameMode.changed -= HandleGameModeChanged;

    }

    public void HandleCurrentChanged(SpiceSharp.Simulations.IBiasingSimulation sim)
    {
        try
        {
            var id = gameObject.GetInstanceID().ToString();
            var current = new SpiceSharp.Simulations.RealPropertyExport(sim, id, "i").Value;
            led.SetIntensity(CurrentToIntensity((float)(current * 1000)));

            if (current > MAX_CURRENT)
            {
                FeebackTerminal.Write(new Log("LED current dangerously high!", LogType.Error));
                gameObject.AddComponent<LEDBurnAnimation>();
            }
            else if (System.Math.Abs(current - OPTIMAL_CURRENT) > ACCEPTABLE_CURRENT_DIFF) FeebackTerminal.Write(new Log($"LED current too {(current > OPTIMAL_CURRENT ? "high" : "low")}.", LogType.Warning));
        }
        catch (BehaviorsNotFoundException e)
        {
            Debug.Log("LED not connected: " + e);
        }
    }

    public void HandleGameModeChanged(State newState)
    {
        if (newState is Edit)
        {
            led.SetIntensity(0);
        }
    }
}
