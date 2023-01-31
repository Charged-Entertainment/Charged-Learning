using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public partial class Camera : Singleton<Camera>
{
    public partial class Controller
    {
        public class Pan : Singleton<Pan>
        {
            private Vector3 dragOrigin;
            void Update()
            {
                var pos = Utils.GetMouseWorldPosition();
                if (Input.GetMouseButtonDown(2) || (InteractionMode.Current == InteractionModes.Pan && Input.GetMouseButtonDown(0))) dragOrigin = pos;
                if (Input.GetMouseButton(2) || (InteractionMode.Current == InteractionModes.Pan && Input.GetMouseButton(0))) Camera.Pan(dragOrigin - pos);
            }
        }
    }
}