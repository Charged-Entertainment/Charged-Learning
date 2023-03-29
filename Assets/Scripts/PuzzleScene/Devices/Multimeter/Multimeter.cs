using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Components;
public class Multimeter : Device, CircuitComponent
{
    [SerializeField] private float testDisplayValue;
    [SerializeField] private Display display;
    [SerializeField] private bool turnedOn;
    public bool InCircuit { get; set; }
    [field: SerializeField] public Terminal[] Terminals { get; private set; }

    public static Action created, destroyed;
    public static Action<MultimeterMode> modeChanged;
    public static Action<double, PropertyType> measured;
    public LiveComponent ConnectedComponent { get; set; }

    private static string prefabName = "Multimeter Device";

    private void Awake()
    {
        created?.Invoke();
        Terminals = gameObject.GetComponentsInChildren<Terminal>(true);

    }

    private void OnDestroy()
    {
        destroyed?.Invoke();
    }

    private void Start()
    {
        display = transform.Find("Multimeter").gameObject.AddComponent<Display>();
        transform.Find("Multimeter").gameObject.AddComponent<Body>();
        DeviceMode = new OffMode();
        turnedOn = false;
        DeviceMode.OnEnter(this);
    }
    public override void ChangeMode(DeviceMode newMode)
    {
        DeviceMode.OnExit();
        newMode.OnEnter(this);
        DeviceMode = newMode;
        modeChanged?.Invoke((MultimeterMode)newMode);
    }
    private void Update()
    {
    }

    public void DisplayMessage(string message)
    {
        display.Write(message);
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

    public SpiceSharp.Components.Component GetSpiceComponent(string positiveWire, string negativeWire)
    {
        Debug.Log(RichText.Color("Multimeter get spice called", PaletteColor.Red));
        string id = gameObject.GetInstanceID().ToString();
        if (DeviceMode is VoltageMode)
        {
            return new SpiceSharp.Components.Resistor(
                id,
                positiveWire,
                negativeWire,
                10e6
                );
        }
        else if (DeviceMode is CurrentMode)
        {
            return new SpiceSharp.Components.VoltageSource(
            id,
            positiveWire,
            negativeWire,
            0
            );

        }
        else if (DeviceMode is ResistanceMode)
        {
            {
                return new SpiceSharp.Components.VoltageSource(
                id,
                positiveWire,
                negativeWire,
                1
                );
            }

        }
        else
            throw new NotImplementedException("Set the multimeter to current or voltage modes for now");
    }

    public string ID() { return gameObject.GetInstanceID().ToString(); }

    public static void Spawn()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene != null) Debug.Log("Multimeter already in scene, cannot spawn.");
        else GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/Devices/Multimeter/{prefabName}")).name = prefabName;
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
        else
        {
            // unsubscribe the HandleSimulation handler
            inScene.GetComponent<Multimeter>().DeviceMode.OnExit();
            GameObject.Destroy(inScene);
        }
    }

    public static Multimeter Get() {
        return GameObject.Find(prefabName)?.GetComponent<Multimeter>();
    }

    public static EditorBehaviour GetBody() {
        var inScene = GameObject.Find(prefabName);
        if (inScene == null) return null;
        return inScene.transform.Find("Multimeter").GetComponent<Body>();
    }

    public static Probe GetRedProbe() {
        var inScene = GameObject.Find(prefabName);
        if (inScene == null) return null;
        return inScene.transform.Find("multimeter-wire-red").GetComponent<Probe>();
    }

    public static Probe GetBlackProbe() {
        var inScene = GameObject.Find(prefabName);
        if (inScene == null) return null;
        return inScene.transform.Find("multimeter-wire-black").GetComponent<Probe>();
    }
}
