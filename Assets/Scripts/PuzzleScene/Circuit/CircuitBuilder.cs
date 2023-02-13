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
        if (terminalToWire.ContainsKey(wire.t1) && terminalToWire.ContainsKey(wire.t2)){
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


    public void Collect(){}
}
