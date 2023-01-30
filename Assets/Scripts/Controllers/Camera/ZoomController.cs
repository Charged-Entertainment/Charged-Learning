using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class ZoomController : Controller<ZoomController>
    {
        private void Update()
        {
            float scrollData;
            scrollData = Input.GetAxis("Mouse ScrollWheel");
            if (scrollData != 0) Camera.Zoom(scrollData);
        }
    }
}