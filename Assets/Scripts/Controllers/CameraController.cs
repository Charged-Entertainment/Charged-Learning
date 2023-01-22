using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Controller
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
    private Vector3 dragOrigin;


    void Update()
    {
        HandleZoom();
        HandlePan();
    }

    private void HandleZoom()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
    private void HandlePan()
    {
        //save position when mouse is clicked 
        if (Input.GetMouseButtonDown(0))
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        //calculate distance between origin and drag position 
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            print("origin " + dragOrigin + " newPosition" + cam.ScreenToWorldPoint(Input.mousePosition) + " =difference " + difference);

            //move the camera by that distance 
            cam.transform.position += difference;
        }

        //move the camera by that distance 

    }
}
