using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanController : Controller
{
    private Vector3 dragOrigin;
    void Update()
    {
        var mousePosition = Utils.GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(2)) dragOrigin = mousePosition;
        if (Input.GetMouseButton(2)) mainManager.cameraManager.Pan(dragOrigin - mousePosition);
    }
}
