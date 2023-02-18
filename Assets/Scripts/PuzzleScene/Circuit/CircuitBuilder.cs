using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using System;
using System.Linq;

public class CircuitBuilder : Singleton<CircuitBuilder>
{
    // Start is called before the first frame update
    private static Dictionary<Terminal, Wire> finalCircuit;
    private static Dictionary<Terminal, Dictionary<Terminal, Wire>> circuitGraph;
    private static Wire groundWire;
    protected new void Awake()
    {
        base.Awake();
        finalCircuit = new Dictionary<Terminal, Wire>();
        circuitGraph = new Dictionary<Terminal, Dictionary<Terminal, Wire>>();
        Wire.created += HandleWireCreated;
        Wire.destroyed += HandleWireDestroyed;
    }

    private void HandleWireDestroyed(Wire wire)
    {
        Debug.Log("Wire destroyed");

        circuitGraph[wire.t1].Remove(wire.t2);
        circuitGraph[wire.t2].Remove(wire.t1);

        if(circuitGraph[wire.t1].Count == 0)
            circuitGraph.Remove(wire.t1);
        if(circuitGraph[wire.t2].Count == 0)
            circuitGraph.Remove(wire.t2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HandleWireCreated(Wire wire)
    {
        if (!circuitGraph.ContainsKey(wire.t1))
        {
            circuitGraph[wire.t1] = new Dictionary<Terminal, Wire>();
        }
        circuitGraph[wire.t1].Add(wire.t2, wire);


        if (!circuitGraph.ContainsKey(wire.t2))
        {
            circuitGraph[wire.t2] = new Dictionary<Terminal, Wire>();
        }
        circuitGraph[wire.t2].Add(wire.t1, wire);
    }

    /// <summary>Performs a DFS traversal of the graph to create the final circuit represintation</summary>
    private static void CreateFinalCircuit(){
        finalCircuit.Clear();
        var nodesVisited = new HashSet<Terminal>();
        foreach(var node in circuitGraph){
            TraverseNode(node.Key, nodesVisited);
        }
            
    }
    private static void TraverseNode(Terminal startingNode, HashSet<Terminal> nodesVisited)
    {
        var stack = new Stack<Terminal>();
        stack.Push(startingNode);
        while(stack.Count > 0)
        {
            var terminal = stack.Pop();
            if(!nodesVisited.Contains(terminal)){
                nodesVisited.Add(terminal);

                foreach(var neighbor in circuitGraph[terminal]){
                    if(finalCircuit.ContainsKey(terminal) && finalCircuit.ContainsKey(neighbor.Key)){
                        Debug.Log("Both terminals are already in dict");
                    }
                    else if(finalCircuit.ContainsKey(terminal)){
                        finalCircuit.Add(neighbor.Key, finalCircuit[terminal]);
                    }else if(finalCircuit.ContainsKey(neighbor.Key)){
                        //this case probably never happens
                        Debug.Log("neighbor in finalCircuit");
                        finalCircuit.Add(terminal, finalCircuit[neighbor.Key]);
                    }else{
                        finalCircuit.Add(terminal, neighbor.Value);
                        finalCircuit.Add(neighbor.Key, neighbor.Value);
                    }
                    stack.Push(neighbor.Key);
                }
            }
        }
    }

    private static HashSet<CircuitComponent> GetCircuitComponents()
    {
        var circuitComponents = new HashSet<CircuitComponent>();
        Multimeter multimeter = null;
        foreach (var terminal in finalCircuit)
        {
            circuitComponents.Add(terminal.Key.parent);
            if (terminal.Key.parent is Multimeter)
            {
                multimeter = terminal.Key.parent as Multimeter;
                var multimeterTerminal1 = circuitGraph[multimeter.Terminals[0]].First().Key;
                var multimeterTerminal2 = circuitGraph[multimeter.Terminals[1]].First().Key;
                if(multimeterTerminal1.parent == multimeterTerminal2.parent)
                    multimeter.ConnectedComponent = (multimeterTerminal1.parent as LiveComponent);
                Debug.Log($"Multimeter is in circuit: {multimeter}");
                multimeter.InCircuit = true;
            }
        }

        return circuitComponents;
    }

    public static SpiceSharp.Circuit Collect()
    {
        CreateFinalCircuit();

        var circuitComponents = GetCircuitComponents();

        FindGroundWire();


        var circuit = new SpiceSharp.Circuit();

        foreach (var circuitComponent in circuitComponents)
        {
            var entity = circuitComponent.GetSpiceComponent(
                GetWireName(finalCircuit[circuitComponent.Terminals[0]]),
                GetWireName(finalCircuit[circuitComponent.Terminals[1]])
                );

            circuit.Add(entity);
        }

        return circuit;
    }

    public static string GetNode(Terminal t)
    {
        if (finalCircuit.ContainsKey(t)) return GetWireName(finalCircuit[t]);
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

        foreach (var pair in finalCircuit)
        {
            // TODO: find a way to make this more generic since more than 1 battery can be in the scene.
            var component = pair.Key.parent as LiveComponent;
            if (component != null && component.levelComponent.Component.componentType == ComponentType.Battery && pair.Key.name == "negative_terminal")
            {
                groundWire = pair.Value;
                return pair.Value;
            }
        }
        var multimeter = GameObject.FindObjectOfType<Multimeter>();
        if(multimeter.InCircuit && multimeter.DeviceMode is ResistanceMode){
            groundWire = finalCircuit[multimeter.Terminals[0]];
            return groundWire;
        }else
            Debug.Log($"Multimeter.InCircuit:{multimeter.InCircuit}, deviceMode: {multimeter.DeviceMode}");
        
        Debug.Log("No wire in the circuit can be considered as ground.");
        return null;
    }

}
