using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Camera : Singleton<Camera>
{
    public partial class Controller
    {
        public class Zoom : Singleton<Zoom>
        {
            private void Update()
            {
                float scrollData;
                scrollData = Input.GetAxis("Mouse ScrollWheel");
                if (scrollData != 0) Camera.Zoom(scrollData);
            }
        }
    }
}