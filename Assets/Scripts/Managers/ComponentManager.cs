using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComponentManager : Manager
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    public Action<ComponentBehavior> componentMouseEntered;
    public Action<ComponentBehavior> componentMouseExited;
    public Action<ComponentBehavior> componentMouseDown;
    public Action<ComponentBehavior> componentMouseUp;
    public Action<ComponentBehavior> componentDragged;
}
