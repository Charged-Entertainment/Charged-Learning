using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Camera : Singleton<Camera>
{
    public partial class Controller
    {
        public class Pan : Singleton<Pan>
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
}