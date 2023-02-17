using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Components;

public class Wire : MonoBehaviour
{
    public static Action<Wire> created;
    public static Action<Wire> destroyed;
    public Terminal t1, t2;

    private LineRenderer lineRenderer;
    private new MeshCollider collider;
    [SerializeField] private List<Transform> points;

    private void Awake()
    {
        lineRenderer = Instantiate((GameObject)Resources.Load("LineRenderer"), transform).GetComponent<LineRenderer>();
        collider = gameObject.AddComponent<MeshCollider>();
        points = new List<Transform>();
        points.Add(null);
        points.Add(null);
    }

    public void SetTerminals(Terminal t1, Terminal t2)
    {
        this.t1 = t1;
        this.t2 = t2;
        points[0] = t1.transform;
        points[1] = t2.transform;
        created?.Invoke(this);
    }

    // Optimization possible: do this only on Component.moved instead of every frame.
    private void Update()
    {
        Vector3[] positions = new Vector3[points.Count];
        for (int i = 0; i < points.Count; i++) positions[i] = (Vector2)points[i].position;
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(positions);

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }

    Vector3? mouseDownPos = null;
    bool canSpawnPoint = true;
    WireHelperPoint newlySpawnedPoint = null;
    private void OnMouseDown()
    {
        mouseDownPos = Utils.GetMouseWorldPosition();
    }

    private void OnMouseOver()
    {
        bool mouseIsDown = mouseDownPos != null;
        var mousePos = Utils.GetMouseWorldPosition();
        bool mouseMovedSinceDown = mouseDownPos != Utils.GetMouseWorldPosition();

        if (mouseIsDown && canSpawnPoint && mouseMovedSinceDown)
        {
            newlySpawnedPoint = CreatePoint(mousePos);
            newlySpawnedPoint.followMouse = true;
            canSpawnPoint = false;
        }
    }

    private void OnMouseUp()
    {
        if (Utils.GetMouseWorldPosition() == mouseDownPos) t1.Disconnect(t2);
        mouseDownPos = null;
        canSpawnPoint = true;
        if (newlySpawnedPoint != null) {
            newlySpawnedPoint.followMouse = false;
            newlySpawnedPoint = null;
        }
        
    }


    private WireHelperPoint CreatePoint(Vector2 mousePos)
    {
        var prefab = Resources.Load<GameObject>("WireHelperPoint");
        var point = GameObject.Instantiate<GameObject>(prefab, lineRenderer.transform).GetComponent<WireHelperPoint>();
        point.transform.position = mousePos;
        points.Insert(GetIndexOf2ndClosestPoint(point.transform), point.transform);
        return point;
    }

    private int GetIndexOf2ndClosestPoint(Transform t)
    {
        float smallestDist = float.MaxValue, secondSmallestDist = float.MaxValue;
        int smallestDistIdx = -1, secondSmallestDistIdx = -1;
        for (int i = 0; i < points.Count; i++)
        {
            var dist = Mathf.Abs((points[i].position - t.position).magnitude);
            if (dist < smallestDist) {
                secondSmallestDist = smallestDist;
                secondSmallestDistIdx = smallestDistIdx;
                smallestDist = dist;
                smallestDistIdx = i;    
            }
            else if (dist < secondSmallestDist) {
                secondSmallestDist = dist;
                secondSmallestDistIdx = i;
            }
        }

        // sometimes they're swapped for some reason
        return Mathf.Max(smallestDistIdx, secondSmallestDistIdx);
    }

    public void RemovePoint(Transform t)
    {
        points.Remove(t);
    }

    private void OnDestroy()
    {
        destroyed?.Invoke(this);
    }
}
