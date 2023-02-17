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

    private void Awake()
    {
        lineRenderer = Instantiate((GameObject)Resources.Load("LineRenderer"), transform).GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        collider = gameObject.AddComponent<MeshCollider>();
    }

    public void SetTerminals(Terminal t1, Terminal t2)
    {
        this.t1 = t1;
        this.t2 = t2;
        created?.Invoke(this);
    }

    // Optimization possible: do this only on Component.moved instead of every frame.
    private void Update()
    {
        lineRenderer.SetPosition(0, t1.transform.position);
        lineRenderer.SetPosition(1, t2.transform.position);

        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }

    Vector3 mouseDownPos;
    private void OnMouseDown() {
        mouseDownPos = Utils.GetMouseWorldPosition();
    }

    private void OnMouseUp() {
        if (Utils.GetMouseWorldPosition() == mouseDownPos) {
            t1.Disconnect(t2);
        }
    }

    private void OnDestroy()
    {
        destroyed?.Invoke(this);
    }
}
