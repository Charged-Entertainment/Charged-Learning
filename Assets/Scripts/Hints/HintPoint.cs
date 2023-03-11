using System.Linq;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A singular point used by a Hint object. Can be a static point or the position of a transform. 
/// </summary>
public class HintPoint
{
    private Transform transform = null;
    private Vector3 _position;
    public Vector3 position {
        get {
            if (transform != null) return transform.position;
            else return _position;
        }
    }

    /// <param name="t">A transform to get the position from.</param>
    public HintPoint(Transform t)
    {
        transform = t;
    }

    /// <param name="p">A static point used as the position.</param>
    public HintPoint(Vector3 p)
    {
        _position = p;
    }
    public static implicit operator HintPoint(Vector3 v) { return new HintPoint(v); }
    public static implicit operator HintPoint(Transform t) { return new HintPoint(t); }
    // public static implicit operator Vector3(HintPoint p) { return p.Position; }
    // public static implicit operator HintPoint(List<HintPoint> t) { return t[0]; }
}