using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : Controller
{
    public ClipboardController clipboardController;
    public SelectionController selectionController;
    public TransformationController transformationController;
    public DragController dragController;
    public PanController panController;
    public ZoomController zoomController;

    protected override void Awake()
    {
        clipboardController = gameObject.AddComponent<ClipboardController>();
        selectionController = gameObject.AddComponent<SelectionController>();
        
        // Transformation controllers
        transformationController = gameObject.AddComponent<TransformationController>();
        dragController = gameObject.AddComponent<DragController>();
        
        // Camera controllers
        zoomController = gameObject.AddComponent<ZoomController>();
        panController = gameObject.AddComponent<PanController>();
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
