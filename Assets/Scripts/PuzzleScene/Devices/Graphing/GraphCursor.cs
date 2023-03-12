using UnityEngine;

/// <summary>
/// Attach to graphs to move along them and read values from them.
/// </summary>
public class GraphCursor : MonoBehaviour
{
    public int Index { get; private set; } = 0;
    public bool SnapedToEnd { get; private set; } = true;
    public bool Attached { get { return graph != null; } }
    public float CurrentValue { get { return graph.At(Index).y; } }
    public float CurrentTime { get { return graph.At(Index).x; } }
    public Vector2 CurrentTimeAndValue { get { return graph.At(Index); } }
    Graph graph;

    private void Awake()
    {
        // try to attach to any graph
        graph = GameObject.FindObjectOfType<Graph>();
    }

    private void Update()
    {
        if (graph != null)
        {
            if (SnapedToEnd) Index = graph.LastIndex;
            MoveToIndex(Index);
        }
    }

    void MoveToIndex(int i)
    {
        var pos = graph.At(i);
        var scale = graph.Scale;
        transform.position = new Vector3(pos.x * scale.x, pos.y * scale.y, transform.position.z);
        transform.position += graph.transform.position;
        SnapedToEnd = i == graph.LastIndex;
    }

    /// <summary>
    /// Move i indices along the graph, or move directly to i if isOffset is false;
    /// </summary>
    public void Move(int i, bool isOffset = true)
    {
        SnapedToEnd = false;
        if (isOffset) Index += i;
        else Index = i;
        Index = Mathf.Clamp(Index, 0, graph.LastIndex);
        MoveToIndex(Index);
    }

    /// <summary>
    /// Attach to the given graph. Will de-attach from old graph if there was one.
    /// </summary>
    public void Attach(Graph graph)
    {
        this.graph = graph;
    }

    /// <summary>
    ///[NOT IMPLEMENTED YET] Attach to the next graph available in Graphing.
    /// </summary>
    public void JumpToNextGraph(Graph graph)
    {
        // Attach(Graphing.Graphs[(indexof(graph) + 1) % Graphing.Graphs.Count]);
    }

    /// <summary>
    ///[NOT IMPLEMENTED YET] Attach to the previous graph available in Graphing. 
    /// </summary>
    public void JumpToPreviousGraph(Graph graph)
    {
        // Attach(Graphing.Graphs[(indexof(graph) - 1) % Graphing.Graphs.Count]);
    }
}
