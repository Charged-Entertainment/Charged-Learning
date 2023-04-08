using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Components;
using GameManagement;
using CLGraphing;

public class GraphingTool : Device, CircuitComponent
{
    public static GraphingTool instance;


    public static GraphingToolDisplay display { get; private set; }
    // [SerializeField] private bool turnedOn;
    public bool InCircuit { get; set; } = true;
    [field: SerializeField] public Terminal[] Terminals { get; private set; }

    public static Action created, destroyed;
    public static Action<GraphingToolMode> modeChanged;
    public static Action<double, PropertyType> measured;
    public LiveComponent ConnectedComponent { get; set; }

    private static string prefabName = "Graphing";
    private Graphing graphing;

    private void Awake()
    {
        if (instance == null) instance = this;
        else GameObject.Destroy(this);

        Terminals = gameObject.GetComponentsInChildren<Terminal>(true);
        created?.Invoke();
    }

    private void OnDestroy()
    {
        destroyed?.Invoke();
    }

    private void Start()
    {
        display = transform.Find("GraphingTool/Display").gameObject.AddComponent<GraphingToolDisplay>();
        transform.Find("GraphingTool").gameObject.AddComponent<GraphingToolBody>();
        currentMode = new GraphingToolVoltageMode(false, "200");
        // turnedOn = false;
        currentMode.OnEnter(this);
        graphing = gameObject.AddComponent<Graphing>();
        GameMode.changed += HandleGameModeChanged;
    }

    private void HandleGameModeChanged(GameModes mode)
    {
        if (mode != GameModes.Edit)
        {
            switch (currentMode)
            {
                case GraphingToolVoltageMode:
                case GraphingToolCurrentMode:
                case GraphingToolResistanceMode:
                case GraphingToolOffMode:
                Graphing.CreateGraph();
                break;
                default:
                break;
            }
        }
    }

    public override void ChangeMode(DeviceMode newMode)
    {
        instance.currentMode.OnExit();
        newMode.OnEnter(instance);
        instance.currentMode = newMode;
        modeChanged?.Invoke((GraphingToolMode)newMode);
    }

    public static void ChangeVoltageMode()
    {
        instance.ChangeMode(new GraphingToolVoltageMode(false, "200"));
    }

    public static void ChangeToCurrentMode()
    {
        instance.ChangeMode(new GraphingToolCurrentMode(false, "200"));
    }

    public static void ChangeToResistanceMode()
    {
        instance.ChangeMode(new GraphingToolResistanceMode("200"));
    }

    public static void ChangeToOffMode()
    {
        instance.ChangeMode(new GraphingToolOffMode());
    }

    public static void Clear()
    {
        display.TurnOff();
    }

    public static void Plot(Vector2 point) {
        Graphing.AddPoint(point);
    }


    public void TurnOff()
    {
        // turnedOn = false;
        display.TurnOff();
    }

    public void TurnOn()
    {
        // turnedOn = true;
    }

    public SpiceSharp.Entities.Entity[] GetSpiceComponent(string positiveWire, string negativeWire)
    {
        Debug.Log(RichText.Color("GraphingTool get spice called", PaletteColor.Red));
        string id = gameObject.GetInstanceID().ToString();
        if (currentMode is GraphingToolVoltageMode)
        {
            return new SpiceSharp.Entities.Entity[] { new SpiceSharp.Components.Resistor(
                id,
                positiveWire,
                negativeWire,
                10e6
                )};
        }
        else if (currentMode is GraphingToolCurrentMode)
        {
            return new SpiceSharp.Entities.Entity[] { new SpiceSharp.Components.VoltageSource(
            id,
            positiveWire,
            negativeWire,
            0
            )};

        }
        else if (currentMode is GraphingToolResistanceMode)
        {
            {
                return new SpiceSharp.Entities.Entity[] { new SpiceSharp.Components.VoltageSource(
                id,
                positiveWire,
                negativeWire,
                1
                )};
            }

        }
        else
            throw new NotImplementedException("Set the multimeter to current or voltage modes for now");
    }

    public string ID() { return gameObject.GetInstanceID().ToString(); }

    public static void Spawn()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene != null) Debug.Log("GraphingTool already in scene, cannot spawn.");
        else GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/Devices/GraphingTool/{prefabName}")).name = prefabName;
    }

    public static bool IsAvailable()
    {
        var inScene = GameObject.Find(prefabName);
        return inScene != null;
    }

    public static void Destroy()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene == null) Debug.Log("GraphingTool not in scene, cannot destroy.");
        else
        {
            // unsubscribe the HandleSimulation handler
            inScene.GetComponent<GraphingTool>().currentMode.OnExit();
            GameObject.Destroy(inScene);
        }
    }

    public static GraphingTool Get()
    {
        return instance;
    }

    public static EditorBehaviour GetBody()
    {
        if (instance == null) return null;
        return instance.transform.Find("GraphingTool").GetComponent<GraphingToolBody>();
    }

    public static Probe GetRedProbe()
    {
        if (instance == null) return null;
        return instance.transform.Find("graphing-wire-red").GetComponent<Probe>();
    }

    public static Probe GetBlackProbe()
    {
        if (instance == null) return null;
        return instance.transform.Find("graphing-wire-black").GetComponent<Probe>();
    }
}
