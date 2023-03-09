using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private static readonly float MAX_INTENSITY = 10.0f;
    private static readonly float SPEED = 10.0f;
    private static readonly float DELAY = 1f;
    private new PointLight light;
    private bool loop;
    private List<HintPoint> points;
    private bool shouldAnimate;
    private void Awake()
    {
        light = gameObject.GetComponent<PointLight>();
        light.SetIntensityImmediate(0, false);
    }

    private HintPoint nextPoint;
    private int nextPointIdx;
    private void Init()
    {
        light.SetIntensity(0);
        shouldAnimate = false;
        Invoke("StartAnimation", DELAY);
    }

    private void StartAnimation()
    {
        transform.position = points[0].position;
        light.SetIntensity(MAX_INTENSITY);
        if (points.Count == 1) Invoke("EndCycle", DELAY);
        else
        {
            nextPointIdx = 1;
            nextPoint = points[1];
            shouldAnimate = true;
        }
    }

    private void Update()
    {
        if (shouldAnimate)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint.position, SPEED * Time.deltaTime);
            if (Utils.Approximately(transform.position, nextPoint.position)) Progress();
        }
    }

    private void Progress()
    {
        if (nextPointIdx == points.Count - 1) EndCycle();
        else
        {
            nextPointIdx++;
            nextPoint = points[nextPointIdx];
        }
    }

    private void EndCycle()
    {
        if (loop) Init();
        else EndGracefully();
    }
    private void _Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void EndGracefully()
    {
        light.SetIntensity(0);
        Invoke("_Destroy", DELAY);
    }

    private static Hint _CreateHint(bool once, IList<HintPoint> points)
    {
        var hint = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Hint")).GetComponent<Hint>();
        hint.points = points.ToList();
        hint.loop = !once;
        hint.Init();
        return hint;
    }


    #region Hint API
    /// <summary>
    /// Create a single-point hint.
    /// </summary>
    /// <param name="point"> A Transform or a Vector3 to display the hint at. </param>
    /// <param name="once"> Whether the hint should repeat. If false, the hint will need to be manually destroyed.</param>
    public static Hint CreateHint(HintPoint point, bool once)
    {
        return _CreateHint(once, new HintPoint[1] { point });
    }

    public static Hint CreateHint(GameObject point, bool once)
    {
        return _CreateHint(once, new HintPoint[1] { point.transform });
    }

    /// <summary>
    /// Create a hint from a list of points.
    /// </summary>
    public static Hint CreateHint(IList<HintPoint> points, bool once)
    {
        return _CreateHint(once, points);
    }

    /// <summary>
    /// Create a hint from a list of Transforms.
    /// </summary>
    public static Hint CreateHint(IList<Transform> transforms, bool once)
    {
        var points = new List<HintPoint>();
        foreach (var t in transforms) points.Add(t);
        return _CreateHint(once, points);
    }

    /// <summary>
    /// Create a hint from a list of gameobjects, the center of their Transforms will be used.
    /// </summary>
    public static Hint CreateHint(IList<GameObject> objects, bool once)
    {
        var points = new List<HintPoint>();
        foreach (var t in objects.Select(e => e.transform)) points.Add(t);
        return _CreateHint(once, points);
    }

    /// <summary>
    /// Create a hint from a list of static positions.
    /// </summary>
    public static Hint CreateHint(IList<Vector3> points, bool once)
    {
        var hintpoints = new List<HintPoint>();
        foreach (var t in points) hintpoints.Add(t);
        return _CreateHint(once, hintpoints);
    }

    /// <summary>
    /// Create a hint from a list of positions, each being either a static Vector3, or a Transform to use the position from. Pass as many points as you'd like as arguments.
    /// </summary>
    public static Hint CreateHint(bool once, params HintPoint[] points)
    {
        return _CreateHint(once, points);
    }
    #endregion
}
