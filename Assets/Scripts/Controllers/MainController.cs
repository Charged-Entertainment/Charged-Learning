using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : Controller
{
    ClipboardController clipboardController;
    SelectionController selectionController;
    TransformationController transformationController;
    PanController panController;
    ZoomController zoomController;

    protected override void Awake()
    {
        clipboardController = gameObject.AddComponent<ClipboardController>();
        selectionController = gameObject.AddComponent<SelectionController>();
        transformationController = gameObject.AddComponent<TransformationController>();
        zoomController = gameObject.AddComponent<ZoomController>();
        panController = gameObject.AddComponent<PanController>();
        
        // Temp
        DisableController(panController);
    }

    public void EnableController(Controller controller)
    {
        controller.Enable();
    }

    public void DisableController(Controller controller)
    {
        controller.Disable();
    }
}
