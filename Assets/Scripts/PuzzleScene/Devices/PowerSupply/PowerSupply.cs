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
}
