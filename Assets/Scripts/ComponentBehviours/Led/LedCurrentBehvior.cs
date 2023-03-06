using UnityEngine;
using SpiceSharp;
using SpiceSharp.Simulations;
using GameManagement;

public class LedCurrentBehvior : MonoBehaviour
{
    private readonly static float CURRENT_TO_INTENSITY_MULT = 0.0375f;
    
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
        var id = gameObject.GetInstanceID().ToString();
        var current = new SpiceSharp.Simulations.RealPropertyExport(sim, id, "i").Value;
        led.SetIntensity(CurrentToIntensity((float)(current * 1000)));

        // raise errors/warnings (icon/animation near LED + feedback terminal notification) if current is above the maximum rating or the recommended rating
    }

    public void HandleGameModeChanged(State newState)
    {
        if (newState is Edit)
        {
            led.SetIntensity(0);
        }
    }
}
