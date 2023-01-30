using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComponentManager : Manager<ComponentManager>
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static public Action<ComponentBehavior> componentMouseEntered;
    static public Action<ComponentBehavior> componentMouseExited;
    static public Action<ComponentBehavior> componentMouseDown;
    static public Action<ComponentBehavior> componentMouseUp;
    static public Action<ComponentBehavior> componentDragged;


    // Handle all that should happen when creating a new component.
    static public ComponentBehavior ComponentInstantiate(ComponentBehavior original, Transform parent)
    {
        ComponentBehavior copy = Instantiate(original, parent);
        
        bool selected = Selection.IsSelected(original);  
        copy.SetSelectedVisible(selected);
        if (selected) Selection.AddComponent(copy);

        return copy;
    }
}
