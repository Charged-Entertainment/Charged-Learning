using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComponentManager : MonoBehaviour
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    public Action<ComponentBehavior> componentMouseEntered;
    public Action<ComponentBehavior> componentMouseExited;
    public Action<ComponentBehavior> componentMouseDown;
    public Action<ComponentBehavior> componentMouseUp;
    public Action<ComponentBehavior> componentDragged;


    SelectionManager sm;
    private void Start()
    {
        sm = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();

        componentMouseDown += SetCursorLastSeen;
        componentDragged += MoveSelectedObjectsOnDrag;
    }

    Vector3 lastSeen;

    void SetCursorLastSeen(ComponentBehavior component)
    {
        lastSeen = Utils.GetMouseWorldPosition();
    }

    void MoveSelectedObjectsOnDrag(ComponentBehavior component)
    {
        var currMousePos = Utils.GetMouseWorldPosition();
        foreach (var obj in sm.GetSelectedComponents())
        {
            obj.transform.position += currMousePos - lastSeen;
        }
        lastSeen = currMousePos;
    }
}
