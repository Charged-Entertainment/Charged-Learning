using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class EComponent : Singleton<EComponent>
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static public Action<ComponentBehavior> mouseEntered;
    static public Action<ComponentBehavior> mouseExited;
    static public Action<ComponentBehavior> mouseDown;
    static public Action<ComponentBehavior> mouseUp;
    static public Action<ComponentBehavior> dragged;

    private DragController dragController;
    private TransformationController transformationController;

    void Start()
    {
        dragController = gameObject.AddComponent<DragController>();
        transformationController = gameObject.AddComponent<TransformationController>();
    }

    static public void SetControllersEnabled(bool enabled)
    {
        Instance.dragController.enabled = enabled;
        Instance.transformationController.enabled = enabled;
    }

    static public void SetDragControllerEnabled(bool enabled)
    {
        Instance.dragController.enabled = enabled;
    }

    static public void SetTransformationControllerEnabled(bool enabled)
    {
        Instance.transformationController.enabled = enabled;
    }


    // Handle all that should happen when creating a new component.
    static public ComponentBehavior Instantiate(ComponentBehavior original, Transform parent)
    {
        ComponentBehavior copy = GameObject.Instantiate(original, parent);

        bool selected = Selection.IsSelected(original);
        copy.SetSelectedVisible(selected);
        if (selected) Selection.AddComponent(copy);

        return copy;
    }
}
