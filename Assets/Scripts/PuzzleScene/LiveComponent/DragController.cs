using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ComponentManager
{
    private class DragController : Controller
    {
        private void OnEnable()
        {
            OnDisable();
            ComponentManager.mouseDown += SetCursorLastSeen;
            ComponentManager.dragged += MoveSelectedObjectsOnDrag;
        }

        private void OnDisable() {
            ComponentManager.mouseDown -= SetCursorLastSeen;
            ComponentManager.dragged -= MoveSelectedObjectsOnDrag;
        }

        Vector3 lastSeen;

        void SetCursorLastSeen(ComponentBehavior component)
        {
            lastSeen = Utils.GetMouseWorldPosition();
        }

        void MoveSelectedObjectsOnDrag(ComponentBehavior component)
        {
            var currMousePos = Utils.GetMouseWorldPosition();

            foreach (var obj in Selection.GetSelectedComponents())
            {
                obj.Move(currMousePos - lastSeen);
            }
            lastSeen = currMousePos;
        }
    }
}
