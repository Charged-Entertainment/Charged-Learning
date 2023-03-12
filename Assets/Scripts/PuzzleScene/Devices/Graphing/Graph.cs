using System;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
    }
    public void DrawPoint(Vector3 point)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
        if (Count > 0) transform.position += Vector3.left * (point.x - At(LastIndex - 1).x);
    }

    public Vector2 Last()
    {
        return lineRenderer.GetPosition(lineRenderer.positionCount - 1);
    }

    public Vector2 At(int i)
    {
        return lineRenderer.GetPosition(i);
    }

    public Vector2 Scale { get { return transform.localScale; } }
    public int Count { get { return lineRenderer.positionCount; } }
    public int LastIndex { get { return lineRenderer.positionCount - 1; } }
    public float AbsoluteMax
    {
        get
        {
            Vector3[] points = new Vector3[Count];
            lineRenderer.GetPositions(points);
            float max = points.Max(e => e.y);
            float min = points.Min(e => e.y);
            return Mathf.Max(Mathf.Abs(max), Mathf.Abs(min));
        }
    }

    public float Max
    {
        get
        {
            Vector3[] points = new Vector3[Count];
            lineRenderer.GetPositions(points);
            return points.Max(e => e.y);
        }
    }

    public float Min
    {
        get
        {
            Vector3[] points = new Vector3[Count];
            lineRenderer.GetPositions(points);
            return points.Min(e => e.y);
        }
    }
}
