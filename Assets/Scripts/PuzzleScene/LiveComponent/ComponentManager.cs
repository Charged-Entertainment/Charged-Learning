using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManagement;

public partial class ComponentManager : Singleton<ComponentManager>
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static public Action<ComponentBehavior> mouseEntered;
    static public Action<ComponentBehavior> mouseExited;
    static public Action<ComponentBehavior> mouseDown;
    static public Action<ComponentBehavior> mouseUp;
    static public Action<ComponentBehavior> dragged;
    static public Action<ComponentBehavior> componentCreated;

    private DragController dragController;
    private TransformationController transformationController;

    void Start()
    {
        dragController = gameObject.AddComponent<DragController>();
        transformationController = gameObject.AddComponent<TransformationController>();
    }

    private void OnEnable()
    {
        OnDisable();
        GameMode.changed += HandleGameModeChange;
        InteractionMode.changed += HandleInteractionModeChange;
    }

    private void OnDisable()
    {
        GameMode.changed -= HandleGameModeChange;
        InteractionMode.changed -= HandleInteractionModeChange;
    }

    private void HandleGameModeChange(GameModes mode)
    {
        HandleGameModeOrInteractionModeChange();
    }

    private void HandleInteractionModeChange(InteractionModes mode)
    {
        HandleGameModeOrInteractionModeChange();
    }

    private void HandleGameModeOrInteractionModeChange()
    {
        if (GameMode.Current != GameModes.Edit)
        {
            dragController.enabled = false;
            transformationController.enabled = false;
            return;
        }

        if (InteractionMode.Current == InteractionModes.Normal)
        {
            dragController.enabled = true;
            transformationController.enabled = true;
        }
        else if (InteractionMode.Current == InteractionModes.Pan)
        {
            dragController.enabled = false;
            transformationController.enabled = true;
        }
        else
        {
            dragController.enabled = false;
            transformationController.enabled = false;
        }
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

    static public ComponentBehavior Instantiate(Components.LevelComponent comp)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            ComponentBehavior copy = new GameObject().AddComponent<ComponentBehavior>();
            copy.levelComponent = comp;
            componentCreated?.Invoke(copy);
            return copy;
        }
        else
        {
            //TODO: emit error event
            throw new Exception("Quantity ");
        }
    }
}
