using UnityEngine;

public class Graphing : Singleton<Graphing>
{

    static public float ReferenceTime = 0f;
    static public float CurrentScale { get { return graph.transform.localScale.x; } }

    static public Graph graph;

    static public GraphController controller;

    static public GraphCursor cursor;


    static public readonly float MAX_HEIGHT = 2f;
    static public readonly float MAX_WIDTH = 4f;

    private new void Awake()
    {
        base.Awake();
        graph = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/Graph").GetComponent<Graph>());
        // cursor = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/GraphCursor").GetComponent<GraphCursor>());
        controller = gameObject.AddComponent<GraphController>();

        var sprite = GetComponent<SpriteRenderer>();
    }

    public int mult = 1;
    private void Update()
    {
        var y = mult * Mathf.Sin(Time.time);
        graph.AddPoint(new Vector2(Time.time, y));
    }

    public static void Zoom(float factor)
    {
        // foreach (var graph in graphs)
        graph.Zoom(factor);    
    }

}
