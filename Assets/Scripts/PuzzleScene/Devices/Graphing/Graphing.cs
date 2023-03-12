using UnityEngine;

public class Graphing : Singleton<Graphing>
{

    static public float ReferenceTime = 0f;
    static public float CurrentScale = 0f;

    static public Graph graph;

    static public GraphController controller;

    static public GraphCursor cursor;


    static private float maxHeight = 7.5f;

    private new void Awake()
    {
        base.Awake();
        graph = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/Graph").GetComponent<Graph>());
        cursor = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Graphing/GraphCursor").GetComponent<GraphCursor>());
        controller = gameObject.AddComponent<GraphController>();

        var sprite = GetComponent<SpriteRenderer>();
    }

    public int mult = 1;
    private float max = 0;
    private void Update()
    {
        var y = mult * Mathf.Sin(Time.time);
        var absY = Mathf.Abs(y);
        if (absY > max)
        {
            max = absY;
            AutoScaleYAxis();
        }
        graph.DrawPoint(new Vector2(Time.time, y));
    }

    void AutoScaleYAxis()
    {
        if (max > maxHeight)
        {
            var scale = graph.transform.localScale;
            graph.transform.localScale = new Vector3(scale.x, maxHeight / max , scale.z);
        }
    }

    public static void Zoom(float factor)
    {
        var scale = graph.transform.localScale;
        graph.transform.localScale = new Vector3(scale.x * factor, scale.y, scale.z);
    }

}
