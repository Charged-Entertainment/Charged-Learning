using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using System;
using System.Linq;

public class CircuitBuilder : Singleton<CircuitBuilder>
{
    // Start is called before the first frame update
    private static Dictionary<Terminal, Wire> terminalToWire;
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
        Debug.Log("Wire destroyed");
        List<Terminal> ts = new List<Terminal>();
        foreach (var t in terminalToWire) ts.Add(t.Key);

        //BUG: Doesn't delete parallel wires, because they are added to the dictionary as the common wire
        // so "terminalToWire[item] == wire" is never true
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
    }

    private static HashSet<CircuitComponent> GetCircuitComponents(){
        var circuitComponents = new HashSet<CircuitComponent>();
        bool multimeterConnected = false;
        foreach (var terminal in terminalToWire)
        {
            circuitComponents.Add(terminal.Key.parent);
            if(terminal.Key.parent is Multimeter){
                Debug.Log("Multimeter is in circuit");
                multimeterConnected = true;
            }
        }
        var multimeter = GameObject.FindObjectOfType<Multimeter>();
        if(multimeter!=null)multimeter.Connected = multimeterConnected;

        return circuitComponents;
    }

    public static SpiceSharp.Circuit Collect()
    {
        FindGroundWire();

        var circuitComponents = GetCircuitComponents();

        var circuit = new SpiceSharp.Circuit();

        foreach (var circuitComponent in circuitComponents)
        {
            var entity = circuitComponent.GetSpiceComponent(
                GetWireName(terminalToWire[circuitComponent.Terminals[0]]),
                GetWireName(terminalToWire[circuitComponent.Terminals[1]])
                );

            circuit.Add(entity);
        }
        
        return circuit;
    }

    public static string GetNode(Terminal t)
    {
        if (terminalToWire.ContainsKey(t)) return GetWireName(terminalToWire[t]);
        else return null;
    }

    private static string GetWireName(Wire wire)
    {
        if (wire == groundWire) return SpiceSharp.Constants.Ground;
        else return wire.GetInstanceID().ToString();
    }

    private static Wire FindGroundWire()
    {
        if (groundWire != null) return groundWire;

        foreach (var pair in terminalToWire)
        {
            // TODO: find a way to make this more generic since more than 1 battery can be in the scene.
            var component = pair.Key.parent as LiveComponent;
            if (component != null && component.levelComponent.Component.componentType == ComponentType.Battery && pair.Key.name == "negative_terminal")
            {
                groundWire = pair.Value;
                return pair.Value;
            }
        }
        Debug.Log("No wire in the circuit can be considered as ground.");
        return null;
    }

}
