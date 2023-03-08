using UnityEngine;
using System.Linq;

public class ContextMenuController : Controller
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ContextMenuManager.Destroy();
        }
        if (Input.GetMouseButtonUp(1))
        {
            var hit = Physics2D.Raycast(Utils.GetMouseWorldPosition(), Vector2.zero);
            if (!hit) return;
            var contextMenuObject = hit.collider.GetComponent<ContextMenuObject>();
            var editorBehaviour = contextMenuObject as EditorBehaviour;

            if (editorBehaviour != null && !Selection.IsSelected(editorBehaviour))
            {
                Selection.Clear();
                Selection.AddComponent(editorBehaviour);
            }


            ContextMenuManager.CreateMenu(contextMenuObject.GetContextMenuType(), Utils.GetMouseWorldPosition());
        }

        if (Input.GetMouseButtonUp(0))
        {
            ContextMenuManager.Destroy();
        }
    }
}