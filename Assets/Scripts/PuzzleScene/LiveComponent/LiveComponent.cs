using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManagement;
using Components;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Entities;
using System.Text.RegularExpressions;

public partial class LiveComponent : EditorBehaviour, CircuitComponent
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static new public Action<LiveComponent> created;
    static new public Action<LiveComponent> destroyed;
    static public Action<Terminal, Terminal> connected;
    static public Action<Terminal, Terminal> disconnected;

    public Components.LevelComponent levelComponent;

    [field: SerializeField] public Terminal[] Terminals { get; private set; }

    protected override void Awake()
    {
        Terminals = gameObject.GetComponentsInChildren<Terminal>(true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        destroyed?.Invoke(this);
    }

    // TODO: resturn list of components, including needed models
    public SpiceSharp.Entities.Entity[] GetSpiceComponent(string positiveWire, string negativeWire)
    {
        var component = levelComponent.Component;
        var id = gameObject.GetInstanceID().ToString();
        switch (component.componentType)
        {
            case ComponentType.Resistor:
                return new SpiceSharp.Entities.Entity[] {new SpiceSharp.Components.Resistor(
                        id,
                        negativeWire,
                        positiveWire,
                        component.Properties[PropertyType.Resistance].value
                        )};
            case ComponentType.Battery:
                return new SpiceSharp.Entities.Entity[] {new SpiceSharp.Components.VoltageSource(
                       id,
                       negativeWire,
                       positiveWire,
                       component.Properties[PropertyType.Voltage].value
                       )};

            case ComponentType.Led:
                var model = new SpiceSharp.Components.DiodeModel($"${ID()}_model");
                // TODO: set model params (using default diode for now)
                return new SpiceSharp.Entities.Entity[] { new SpiceSharp.Components.Diode(id, negativeWire, positiveWire, model.Name), model };
        }
        return null;

    }

    public string ID() { return gameObject.GetInstanceID().ToString(); }

    // Handle all that should happen when creating a new component.
    static public LiveComponent Instantiate(Components.LevelComponent comp, Transform parent = null, Vector2? pos = null)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            var prefab = Resources.Load<GameObject>($"Prefabs/Components/{comp.Name}");
            LiveComponent copy = GameObject.Instantiate(prefab, parent).GetComponent<LiveComponent>();
            copy.levelComponent = comp;
            if (pos != null) copy.transform.position = (Vector2)pos;
            created?.Invoke(copy);
            return copy;
        }
        else
        {
            throw new Exception("Quantity...");
        }
    }
}