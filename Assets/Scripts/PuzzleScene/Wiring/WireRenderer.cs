using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class WireManager : Singleton<WireManager>
{
    private static Dictionary<Terminal, HashSet<Wire>> TerminalToWires;
    private static Dictionary<string, Wire> TerminalPairToWire;

    private void Start()
    {
        TerminalPairToWire = new Dictionary<string, Wire>();
        TerminalToWires = new Dictionary<Terminal, HashSet<Wire>>();
        gameObject.AddComponent<WireDragPreview>();
    }

    private void OnEnable()
    {
        OnDisable();
        Terminal.connected += Create;
        Terminal.disconnected += Remove;
        Terminal.destroyed += Remove;
    }

    private void OnDisable()
    {
        Terminal.connected -= Create;
        Terminal.disconnected -= Remove;
        Terminal.destroyed -= Remove;
    }

    private string TerminalPairID(Terminal a, Terminal b)
    {
        // needs to sort the 2 objects to be agnostic of order. ie. (a,b)<=>(b,a)
        int id1 = a.GetInstanceID();
        int id2 = b.GetInstanceID();
        return (id1 < id2 ? id1 : id2).ToString() + (id1 < id2 ? id2 : id1).ToString();
    }

    private void Remove(Terminal t)
    {
        HashSet<Wire> wires;
        bool exists = TerminalToWires.TryGetValue(t, out wires);
        if (exists)
        {
            var temp = new List<Wire>();
            foreach (var wire in wires) temp.Add(wire);
            foreach (var wire in temp) Remove(wire);
        }
    }
    private void Remove(Terminal a, Terminal b)
    {
        Wire wire;
        bool exists = TerminalPairToWire.TryGetValue(TerminalPairID(a, b), out wire);
        if (exists) Remove(wire);
    }
    private void Remove(Wire w)
    {
        GameObject.Destroy(w.gameObject);

        HashSet<Wire> h1, h2;
        bool exists1 = TerminalToWires.TryGetValue(w.t1, out h1);
        bool exists2 = TerminalToWires.TryGetValue(w.t2, out h2);

        if (!exists1 || !exists2)
        {
            Debug.Log("Error!");
            return;
        }

        TerminalPairToWire.Remove(TerminalPairID(w.t1, w.t2));
        h1.Remove(w);
        h2.Remove(w);
        if (h1.Count == 0) TerminalToWires.Remove(w.t1);
        if (h2.Count == 0) TerminalToWires.Remove(w.t2);
    }

    private void Create(Terminal t1, Terminal t2)
    {
        var w = new GameObject("Wire").AddComponent<Wire>();
        w.transform.parent = transform;
        w.SetTerminals(t1,t2);

        HashSet<Wire> h1, h2;
        bool exists1 = TerminalToWires.TryGetValue(t1, out h1);
        bool exists2 = TerminalToWires.TryGetValue(t2, out h2);

        if (!exists1)
        {
            h1 = new HashSet<Wire>();
            TerminalToWires.Add(t1, h1);
        }

        if (!exists2)
        {
            h2 = new HashSet<Wire>();
            TerminalToWires.Add(t2, h2);
        }

        TerminalPairToWire.Add(TerminalPairID(t1, t2), w);
        h1.Add(w);
        h2.Add(w);
    }


    private class WireDragPreview : MonoBehaviour
    {
        private LineRenderer lineRenderer;

        private void OnEnable()
        {
            OnDisable();
            Terminal.mouseDown += HandleTerminalMouseDown;
            Terminal.mouseUp += HandleTerminalMouseUp;
        }

        private void OnDisable()
        {
            Terminal.mouseDown -= HandleTerminalMouseDown;
            Terminal.mouseUp -= HandleTerminalMouseUp;
        }

        private void HandleTerminalMouseDown(Terminal t)
        {
            StartIndicator();
        }

        private void HandleTerminalMouseUp(Terminal t)
        {
            StopIndicator();
        }

        public void StartIndicator()
        {
            lineRenderer = Instantiate((GameObject)Resources.Load("LineRenderer")).GetComponent<LineRenderer>();
            lineRenderer.name = "WirePreview";

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, (Vector2)Utils.GetMouseWorldPosition());
        }

        public void StopIndicator()
        {
            GameObject.Destroy(lineRenderer.gameObject);
            lineRenderer = null;
        }

        private void Update()
        {
            if (lineRenderer != null) lineRenderer.SetPosition(1, (Vector2)Utils.GetMouseWorldPosition());
        }
    }
}