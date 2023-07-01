using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EditorBehaviour
{
    private class DragController : Controller
    {
        private void OnEnable()
        {
            OnDisable();
            mouseDown += SetCursorLastSeen;
            dragged += MoveSelectedObjectsOnDrag;
        }

        private void OnDisable() {
            mouseDown -= SetCursorLastSeen;
            dragged -= MoveSelectedObjectsOnDrag;
        }

        Vector3 lastSeen;

        void SetCursorLastSeen(EditorBehaviour component)
        {
            lastSeen = Utils.GetMouseWorldPosition();
        }

        void MoveSelectedObjectsOnDrag(EditorBehaviour component)
        {
            var currMousePos = Utils.GetMouseWorldPosition();

            foreach (var obj in Selection.GetSelectedComponents<EditorBehaviour>())
            {
                obj.Move(currMousePos - lastSeen);
            }
            lastSeen = currMousePos;
        }
    }
}
