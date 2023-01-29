using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : Controller
{
    private void Start()
    {
        mainManager.componentManager.componentMouseDown += SetCursorLastSeen;
        mainManager.componentManager.componentDragged += MoveSelectedObjectsOnDrag;
    }

    Vector3 lastSeen;

    void SetCursorLastSeen(ComponentBehavior component)
    {
        lastSeen = Utils.GetMouseWorldPosition();
    }

    void MoveSelectedObjectsOnDrag(ComponentBehavior component)
    {
        var currMousePos = Utils.GetMouseWorldPosition();

        foreach (var obj in mainManager.selectionManager.GetSelectedComponents())
        {
            obj.transform.position += currMousePos - lastSeen;
        }
        lastSeen = currMousePos;
    }
}
