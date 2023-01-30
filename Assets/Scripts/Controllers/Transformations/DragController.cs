using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class DragController : Controller<DragController>
    {
        private void Start()
        {
            ComponentManager.componentMouseDown += SetCursorLastSeen;
            ComponentManager.componentDragged += MoveSelectedObjectsOnDrag;
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