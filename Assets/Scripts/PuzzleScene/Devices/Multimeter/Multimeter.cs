using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Multimeter : Device
{
    [SerializeField] private float testDisplayValue;
    [SerializeField] private Display display;
    [SerializeField] private bool turnedOn;
    public static Action created;
    public static Action destroyed;

    private static string prefabName = "Multimeter Device";

    private void Awake()
    {
        created?.Invoke();
    }

    private void OnDestroy()
    {
        destroyed?.Invoke();
    }

    protected override void Start()
    {
        base.Start();
        display = transform.Find("Multimeter").gameObject.AddComponent<Display>();
        transform.Find("Multimeter").gameObject.AddComponent<Body>();
        deviceMode = new OffMode();
        turnedOn = false;
        deviceMode.OnEnter(this);
    }
    public override void ChangeMode(DeviceMode newMode)
    {
        deviceMode.OnExit();
        newMode.OnEnter(this);
        deviceMode = newMode;

    }
    private void Update()
    {
        if (turnedOn) DisplayMeasurement(testDisplayValue);
    }

    public void DisplayMeasurement(float measurementValue)
    {
        display.WriteMeasurement(measurementValue);
    }

    public void TurnOff()
    {
        turnedOn = false;
        display.TurnOff();
    }

    public void TurnOn()
    {
        turnedOn = true;
    }

    public static void Spawn()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene != null) Debug.Log("Multimeter already in scene, cannot spawn.");
        else GameObject.Instantiate(Resources.Load<GameObject>($"Devices/{prefabName}")).name = prefabName;
    }

    public static bool IsAvailable()
    {
        var inScene = GameObject.Find(prefabName);
        return inScene != null;
    }

    public static void Destroy()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene == null) Debug.Log("Multimeter not in scene, cannot destroy.");
        else GameObject.Destroy(inScene);
    }
}
