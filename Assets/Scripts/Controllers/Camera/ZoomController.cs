using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : Controller
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

    void Update()
    {
        HandleZoom();
    }

    private void HandleZoom()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}
