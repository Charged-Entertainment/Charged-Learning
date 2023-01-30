using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PanController : Controller<PanController>
    {
        private Vector3 dragOrigin;
        void Update()
        {
            var mousePosition = Utils.GetMouseWorldPosition();
            if (Input.GetMouseButtonDown(2)) dragOrigin = mousePosition;
            if (Input.GetMouseButton(2)) Camera.Pan(dragOrigin - mousePosition);
        }
    }
}