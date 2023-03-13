using System.Collections;
using System.Collections.Generic;
using System;
using Components;
using UnityEngine;

public class PowerSupply : MonoBehaviour, CircuitComponent
{
    public static readonly float MAX_VOLTAGE = 30.0f;
    public static readonly float MAX_CURRENT = 2.0f;
    public static Action created, destroyed;
    PowerSupplyKnob knob;
    [field: SerializeField] public Terminal[] Terminals { get; private set; }
    private void Awake()
    {
        created?.Invoke();
        Terminals = gameObject.GetComponentsInChildren<Terminal>(true);
        knob = gameObject.GetComponentInChildren<PowerSupplyKnob>();
    }

    public SpiceSharp.Components.Component GetSpiceComponent(string positiveWire, string negativeWire)
    {
        string id = gameObject.GetInstanceID().ToString();
        return new SpiceSharp.Components.VoltageSource(
            id,
            positiveWire,
            negativeWire,
            knob.Value * MAX_VOLTAGE
            );
    }

    static readonly string prefabName = "PowerSupply";

    public static void Spawn()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene != null) Debug.Log("PowerSupply already in scene, cannot spawn.");
        else GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/Devices/PowerSupply/{prefabName}")).name = prefabName;
    }

    public static bool IsAvailable()
    {
        var inScene = GameObject.Find(prefabName);
        return inScene != null;
    }

    public static void Destroy()
    {
        var inScene = GameObject.Find(prefabName);
        if (inScene == null) Debug.Log("PowerSupply not in scene, cannot destroy.");
        else GameObject.Destroy(inScene);
    }

    public static Multimeter Get() {
        return GameObject.Find(prefabName)?.GetComponent<Multimeter>();
    }

}
