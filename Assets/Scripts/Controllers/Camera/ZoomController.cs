using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : Controller
{
    private void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (scrollData != 0) mainManager.cameraManager.Zoom(scrollData);
    }
}
