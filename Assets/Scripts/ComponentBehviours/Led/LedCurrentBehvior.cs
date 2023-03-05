using UnityEngine;
using SpiceSharp;
using SpiceSharp.Simulations;
public class LedCurrentBehvior : MonoBehaviour
{
    Led led;
    private void Awake() {
        led = gameObject.GetComponent<Led>();
    }

    private void OnEnable()
    {
        SimulationManager.simulationDone += HandleCurrentChanged;
    }

    private void OnDisable()
    {
        SimulationManager.simulationDone -= HandleCurrentChanged;
    }

    public void HandleCurrentChanged(SpiceSharp.Simulations.IBiasingSimulation sim)
    {
        var id = gameObject.GetInstanceID().ToString();
        var current = new SpiceSharp.Simulations.RealPropertyExport(sim, id, "i").Value;
        led.SetIntensity((float)(current * 1000));
    }
}
