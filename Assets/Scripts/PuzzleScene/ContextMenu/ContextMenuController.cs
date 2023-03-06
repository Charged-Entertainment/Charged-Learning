using UnityEngine;

public class ContextMenuController : Controller{
    private void Update() {
        if(Input.GetMouseButtonDown(1)){
            Debug.Log("Right click down");
            ContextMenuManager.Destroy();
        }
        if(Input.GetMouseButtonUp(1)){
            Debug.Log("Right click up");
            var hits = Physics2D.RaycastAll(Utils.GetMouseWorldPosition(), Vector2.zero);
            foreach(var hit in hits){
                var contextMenuObject = hit.collider.GetComponent<ContextMenuObject>();
                Debug.Log($"contextMenuObject: {contextMenuObject}");
                if(contextMenuObject == null) continue;
                ContextMenuManager.CreateMenu(contextMenuObject, Utils.GetMouseWorldPosition());
            }

        }

        if(Input.GetMouseButtonUp(0)){
            ContextMenuManager.Destroy();
        }
    }
}