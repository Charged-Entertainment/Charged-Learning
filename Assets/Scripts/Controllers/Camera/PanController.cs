using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanController : Controller
{
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }
    private Vector3 dragOrigin;


    void Update()
    {
        HandlePan();
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
