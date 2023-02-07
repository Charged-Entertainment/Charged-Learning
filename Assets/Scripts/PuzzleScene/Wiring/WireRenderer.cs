using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class WireRenderer : Singleton<WireRenderer>
{

    public static LineRenderer LineRenderer { get; private set; }
    private static LineRenderer IndicatorLineRenderer;


    private static HashSet<Terminal> terminals;

    private void Start()
    {
        terminals = new HashSet<Terminal>();
        LineRenderer = Instantiate((GameObject)Resources.Load("LineRenderer")).GetComponent<LineRenderer>();

        IndicatorLineRenderer = Instantiate((GameObject)Resources.Load("LineRenderer")).GetComponent<LineRenderer>();
        // var g = new Gradient();
        // g = IndicatorLineRenderer.colorGradient;
        // for (int i = 0; i < g.alphaKeys.Length; i++)
        // {
        //     g.alphaKeys[i].alpha = 0.3f;
        // }
        // IndicatorLineRenderer.colorGradient = g;
        IndicatorLineRenderer.positionCount = 2;
        IndicatorLineRenderer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnDisable();
        ComponentManager.moved += HandleComponentMoved;
        Terminal.connected += HandleConnected;
        Terminal.disconnected += HandleDisconnected;
        Terminal.mouseDown += HandleTerminalMouseDown;
        Terminal.mouseUp += HandleTerminalMouseUp;
    }

    private void OnDisable()
    {
        ComponentManager.moved -= HandleComponentMoved;
        Terminal.connected -= HandleConnected;
        Terminal.disconnected -= HandleDisconnected;
        Terminal.mouseDown -= HandleTerminalMouseDown;
        Terminal.mouseUp -= HandleTerminalMouseUp;
    }

    private void HandleComponentMoved(MonoBehaviour c)
    {
        WriteToLineRenderer();
    }

    private void HandleConnected(Terminal a, Terminal b)
    {
        AddTerminal(a);
        AddTerminal(b);
    }

    private void HandleDisconnected(Terminal a, Terminal b)
    {
        RemoveTerminal(a);
        RemoveTerminal(b);
    }

    private void HandleTerminalMouseDown(Terminal t)
    {
        StartIndicator();
    }

    private void HandleTerminalMouseUp(Terminal t)
    {
        StopIndicator();
    }

    private static void WriteToLineRenderer()
    {
        LineRenderer.positionCount = terminals.Count;
        int i = 0;
        foreach (var t in terminals)
        {
            LineRenderer.SetPosition(i, t.transform.position);
            i++;
        }
    }
    private void Update()
    {
        if (IndicatorLineRenderer.gameObject.activeSelf)
        {
            IndicatorLineRenderer.SetPosition(1, (Vector2)Utils.GetMouseWorldPosition());
        }
    }
    public static void StartIndicator()
    {
        IndicatorLineRenderer.gameObject.SetActive(true);
        IndicatorLineRenderer.SetPosition(0, (Vector2)Utils.GetMouseWorldPosition());
    }

    public static void StopIndicator()
    {
        IndicatorLineRenderer.gameObject.SetActive(false);
    }

    public static void AddTerminal(Terminal terminal)
    {
        terminals.Add(terminal);
        WriteToLineRenderer();
    }
    public static void RemoveTerminal(Terminal terminal)
    {
        bool removed = terminals.Remove(terminal);
        if (!removed) { Debug.Log("Error"); return; }
        WriteToLineRenderer();
    }
}
