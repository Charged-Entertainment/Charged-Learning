using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public partial class Selection : Singleton<Selection>
{
    /*
    Any operations that should be performed on selected objects will first query
    this class for whatever available selected objects it has, then perform the operation
    on them.
    */

    static private Dictionary<int, ComponentBehavior> selectedGameObjects;

    static private SelectionArea selectionArea = null;

    private SelectionController selectionController;

    private void Start()
    {
        selectedGameObjects = new Dictionary<int, ComponentBehavior>();
        selectionController = gameObject.AddComponent<SelectionController>();
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
        if (GameMode.Current == GameModes.Edit && InteractionMode.Current == InteractionModes.Normal)
            selectionController.enabled = true;
        else selectionController.enabled = false;
    }

    private void HandleInteractionModeChange(InteractionModes mode)
    {
        if (GameMode.Current == GameModes.Edit && InteractionMode.Current == InteractionModes.Normal)
            selectionController.enabled = true;
        else selectionController.enabled = false;
    }

    static public bool OnGoingMultiSelect { get; private set; } = false;

    static public void StartMultiSelect(Vector2 anchor)
    {
        OnGoingMultiSelect = true;
        if (selectionArea == null) selectionArea = Instantiate(((GameObject)Resources.Load("SelectionArea"))).AddComponent<SelectionArea>();
        selectionArea.SetAnchorPoint(anchor);
    }

    static public void AdjuctMultiSelect(Vector2 point)
    {
        selectionArea.Adjust(point);
    }

    static public List<ComponentBehavior> EndMultiSelect()
    {
        var t = selectionArea.GetSelection();
        GameObject.Destroy(selectionArea.gameObject);
        selectionArea = null;
        OnGoingMultiSelect = false;
        return t;
    }

    static public void InvertComponent(ComponentBehavior component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) RemoveComponent(component);
        else
        {
            selectedGameObjects.Add(component.GetInstanceID(), component);
            component.SetSelectedVisible(true);
        }
    }

    static public void AddComponent(ComponentBehavior component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) return;
        else
        {
            selectedGameObjects.Add(component.GetInstanceID(), component);
            component.SetSelectedVisible(true);
        }
    }

    static public void RemoveComponent(ComponentBehavior component)
    {
        selectedGameObjects.Remove(component.GetInstanceID());
        component.SetSelectedVisible(false);
    }

    static public void InvertComponents(List<ComponentBehavior> components)
    {
        foreach (var component in components)
        {
            InvertComponent(component);
        }
    }

    static public void AddComponents(List<ComponentBehavior> components)
    {
        foreach (var component in components)
        {
            AddComponent(component);
        }
    }

    static public void RemoveComponents(List<ComponentBehavior> components)
    {
        foreach (var component in components)
        {
            RemoveComponent(component);
        }
    }

    static public void Clear()
    {
        RemoveComponents(new List<ComponentBehavior>(selectedGameObjects.Values));
        selectedGameObjects.Clear(); // just to make sure
    }

    static public List<ComponentBehavior> GetSelectedComponents(bool includeDisabled = false)
    {
        if (includeDisabled) return new List<ComponentBehavior>(selectedGameObjects.Values);
        return selectedGameObjects.Values.Where(c => c.IsEnabled()).ToList();
    }

    static public bool IsSelected(ComponentBehavior c)
    {
        return selectedGameObjects.ContainsKey(c.GetInstanceID());
    }
}
