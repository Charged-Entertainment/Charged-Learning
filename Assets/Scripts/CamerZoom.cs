using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerZoom : MonoBehaviour
{

    private Camera cam;
    private float targetZoom; 
    private float zoomFactor = 3f;
    private float zoolLerpSpeed = 10;
    void Start()
    {
        
        cam = Camera.main; 
        targetZoom = cam.orthographicSize;


    }


    void Update()
    {
        float scrollData; 
        scrollData = Input.GetAxis("Mouse ScrollWheel"); 

        targetZoom -= scrollData * zoomFactor; 
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoolLerpSpeed); 

    }
}
