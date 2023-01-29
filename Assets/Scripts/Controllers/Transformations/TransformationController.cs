using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : Controller
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.Move(new Vector3(-1f, 0, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.Move(new Vector3(1f, 0, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.Move(new Vector3(0, 1, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.Move(new Vector3(0, -1, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.H) && Input.GetKey(KeyCode.LeftShift))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.FlipH();
            }
        }

        if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftShift))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.FlipV();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
            {
                selectedObject.Rotate(45);
            }
        }

        // TODO: fix this, it crashes Unity...
        // if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
        // {
        //     foreach (var selectedObject in mainManager.selectionManager.GetSelectedComponents())
        //     {
        //         selectedObject.Rotate(-45);
        //     }
        // }
    }
}
