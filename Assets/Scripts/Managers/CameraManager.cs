using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Manager
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 6f;
    private float zoomLerpSpeed = 10;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    public void Pan(Vector3 value, bool isOffset = true)
    {
        if (isOffset) cam.transform.position += value;
        else cam.transform.position = value;
    }
    public void Zoom(float change)
    {
        targetZoom -= change * zoomFactor;
    }

    private void Update()
    {
        if (cam.orthographicSize != targetZoom)
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}
