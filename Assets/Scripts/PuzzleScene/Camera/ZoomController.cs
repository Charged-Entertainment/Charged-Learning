using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Camera : Singleton<Camera>
{
    private class ZoomController : Controller
    {
        private void Update()
        {
            float scrollData;
            scrollData = Input.GetAxis("Mouse ScrollWheel");
            if (scrollData != 0 && !Utils.IsMouseOverUI()) Camera.Zoom(scrollData);
        }
    }
}