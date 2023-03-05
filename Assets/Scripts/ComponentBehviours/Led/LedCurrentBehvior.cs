using UnityEngine;
using SpiceSharp;
using SpiceSharp.Simulations;
using GameManagement;

public class LedCurrentBehvior : MonoBehaviour
{
    Led led;
    private void Awake() {
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
        led.SetIntensity((float)(current * 1000));
    }

    public void HandleGameModeChanged(State newState){
        if(newState is Edit){
            led.SetIntensity(0);
        }
    }
}
