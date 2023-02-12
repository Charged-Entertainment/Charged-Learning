using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManagement;
using Components;

public partial class ComponentManager : Singleton<ComponentManager>
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static public Action<ComponentBehavior> mouseEntered;
    static public Action<ComponentBehavior> mouseExited;
    static public Action<ComponentBehavior> mouseDown;
    static public Action<ComponentBehavior> mouseUp;
    static public Action<ComponentBehavior> dragged;
    static public Action<ComponentBehavior> moved;
    static public Action<ComponentBehavior> created;
    static public Action<ComponentBehavior> destroyed;

    static public Action<Terminal, Terminal> connected;
    static public Action<Terminal, Terminal> disconnected;

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
        else if (InteractionMode.Current == InteractionModes.Wire)
        {
            dragController.enabled = false;
            transformationController.enabled = false;
        }
        else
        {
            Debug.Log("Error: unknown InteractionMode: " + InteractionMode.Current);
        }
    }

    // Handle all that should happen when creating a new component.
    static public ComponentBehavior Instantiate(ComponentBehavior original, Transform parent)
    {
        ComponentBehavior copy = GameObject.Instantiate(original, parent);

        bool selected = Selection.IsSelected(original);
        if (selected) Selection.AddComponent(copy);

        return copy;
    }

    static public ComponentBehavior Instantiate(Components.LevelComponent comp)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            var prefab = Resources.Load<GameObject>(comp.Name);
            ComponentBehavior copy = GameObject.Instantiate(prefab).GetComponent<ComponentBehavior>();
            copy.levelComponent = comp;
            created?.Invoke(copy);
            return copy;
        }
        else
        {
            //TODO: emit error event
            throw new Exception("Quantity ");
        }
    }

    static public ComponentBehavior Instantiate(Components.LevelComponent comp, Vector2 pos)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            var prefab = Resources.Load<GameObject>($"Components/{comp.Name}");
            ComponentBehavior copy = GameObject.Instantiate(prefab).GetComponent<ComponentBehavior>();
            copy.levelComponent = comp;
            copy.transform.position = pos;
            created?.Invoke(copy);
            return copy;
        }
        else
        {
            //TODO: emit error event
            throw new Exception("Quantity ");
        }
    }
}
