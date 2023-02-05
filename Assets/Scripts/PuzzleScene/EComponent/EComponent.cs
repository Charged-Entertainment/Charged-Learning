using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class EComponent : Singleton<EComponent>, IHasControls
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static public Action<ComponentBehavior> mouseEntered;
    static public Action<ComponentBehavior> mouseExited;
    static public Action<ComponentBehavior> mouseDown;
    static public Action<ComponentBehavior> mouseUp;
    static public Action<ComponentBehavior> dragged;

    public List<Controller> Controllers { get; set; }

    void Start()
    {
        Controllers = new List<Controller>();
        Controllers.Add(gameObject.AddComponent<DragController>());
        Controllers.Add(gameObject.AddComponent<TransformationController>());
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