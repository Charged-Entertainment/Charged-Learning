using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : Controller
{
    private void Awake()
    {
        gameObject.AddComponent<ClipboardController>();
        // gameObject.AddComponent<SelectionController>();
        gameObject.AddComponent<TransformationController>();
        gameObject.AddComponent<CameraController>();
    }
}
