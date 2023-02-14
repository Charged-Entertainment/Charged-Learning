using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multimeter : Device
{
    [SerializeField]private float testDisplayValue;
    [SerializeField]private Display display;
    [SerializeField]private bool turnedOn;
    private void Start() {
        display = transform.Find("Multimeter").GetComponent<Display>();
        deviceMode = new OffMode();
        deviceMode.OnEnter(this);
    }
    public override void ChangeMode(DeviceMode newMode)
    {
        deviceMode.OnExit();
        newMode.OnEnter(this);
        deviceMode = newMode;

    }
    private void Update() {
        if(turnedOn)DisplayMeasurement(testDisplayValue);
    }

    public void DisplayMeasurement(float measurementValue){
        display.WriteMeasurement(measurementValue);
    }

    public void TurnOff(){
        turnedOn = false;
        display.TurnOff();
    }

    public void TurnOn(){
        turnedOn = true;
    }

    public static void Spawn()
    {
        var inScene = GameObject.Find("Multimeter Device");
        if (inScene != null) Debug.Log("Multimeter already in scene, cannot spawn.");
        else GameObject.Instantiate(Resources.Load<GameObject>("Devices/Multimeter Device"));
    }

    public static void Destroy()
    {
        var inScene = GameObject.Find("Multimeter Device");
        if (inScene == null) Debug.Log("Multimeter not in scene, cannot destroy.");
        else GameObject.Destroy(inScene);
    }
}
