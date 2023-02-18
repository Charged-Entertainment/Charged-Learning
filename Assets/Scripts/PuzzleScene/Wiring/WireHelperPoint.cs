using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireHelperPoint : EditorBehaviour
{
    Wire parent;
    private void Start()
    {
        parent = transform.parent.parent.GetComponent<Wire>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    bool mouseIsDown = false;

    public bool followMouse = false;
    private new void OnMouseDown()
    {
        mouseIsDown = true;
        base.OnMouseDown();
    }

    private void OnMouseOver()
    {
        if (mouseIsDown || followMouse) transform.position = (Vector2)Utils.GetMouseWorldPosition();
    }

    private new void OnMouseUp()
    {
        mouseIsDown = false;
        base.OnMouseUp();
    }

    private new void OnDestroy()
    {
        base.OnDestroy();
        parent.RemovePoint(transform);
    }
}
