using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using System;
using System.Linq;

public class CircuitBuilder : Singleton<CircuitBuilder>
{
    // Start is called before the first frame update
    private Dictionary<Terminal, Wire> terminalToWire;
    private static Wire groundWire;
    protected new void Awake()
    {
        base.Awake();
        terminalToWire = new Dictionary<Terminal, Wire>();
        Wire.created += HandleWireCreated;
        Wire.destroyed += HandleWireDestroyed;
    }

    private void HandleWireDestroyed(Wire wire)
    {
        List<Terminal> ts = new List<Terminal>();
        foreach (var t in terminalToWire) ts.Add(t.Key);
        foreach (var t in ts.Where((item) => terminalToWire.ContainsKey(item) && terminalToWire[item] == wire)) terminalToWire.Remove(t);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HandleWireCreated(Wire wire)
    {
        if (terminalToWire.ContainsKey(wire.t1) && terminalToWire.ContainsKey(wire.t2))
        {
            Debug.Log("Both terminals are already in the dict");
            return;
        }

        if (terminalToWire.ContainsKey(wire.t1))
        {
            terminalToWire.Add(wire.t2, terminalToWire[wire.t1]);
        }
        else if (terminalToWire.ContainsKey(wire.t2))
        {
            terminalToWire.Add(wire.t1, terminalToWire[wire.t2]);
        }
        else
        {
            terminalToWire.Add(wire.t1, wire);
            terminalToWire.Add(wire.t2, wire);
        }
        foreach (var whatever in terminalToWire)
        {
            Debug.Log($"{whatever.Key.name}:{whatever.Value.GetInstanceID()}");
        }
    }


    public static SpiceSharp.Circuit Collect() {
        var liveComponents = new HashSet<LiveComponent>();
        var terminalToWire = Instance.terminalToWire;
        foreach(var terminal in terminalToWire){
            liveComponents.Add(terminal.Key.parent);
        }

        var circuit = new SpiceSharp.Circuit();

        foreach(var liveComponent in liveComponents){
            var entity = SpiceComponentFactory(liveComponent);
            circuit.Add(entity);
            Debug.Log($"circuit entity: {entity}");
        }
        return circuit;
    }


    /// <summary>Bad hack because Spice simulators require a ground node "0"</summary>
    private static string GetWireName(Wire wire){
        if(groundWire == null){
            groundWire = wire;
            return "0";
        }else if(wire == groundWire)
            return "0";
        else
            return wire.GetInstanceID().ToString();
    }

    private static SpiceSharp.Components.Component SpiceComponentFactory(LiveComponent liveComponent)
    {
        
        var component = liveComponent.levelComponent.Component;
        var terminalToWire = Instance.terminalToWire;

        Debug.Log($"Component Type: {component.componentType}");
        switch (component.componentType)
        {
            case ComponentType.Resistor:
                
                return new SpiceSharp.Components.Resistor(
                    liveComponent.levelComponent.Name,
                    GetWireName(terminalToWire[liveComponent.Terminals[0]]),
                    GetWireName(terminalToWire[liveComponent.Terminals[1]]),
                    component.Properties[PropertyType.Resistance].value
                    );
                
            case ComponentType.Battery:
                return new SpiceSharp.Components.VoltageSource(
                    liveComponent.levelComponent.Name,
                    GetWireName(terminalToWire[liveComponent.Terminals[0]]),
                    GetWireName(terminalToWire[liveComponent.Terminals[1]]),
                    component.Properties[PropertyType.Voltage].value
                    );
        }
        return null;
    }
}
