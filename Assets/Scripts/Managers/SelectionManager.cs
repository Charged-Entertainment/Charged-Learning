using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : Manager
{
    /*
    Any operations that should be performed on selected objects will first query
    this class for whatever available selected objects it has, then perform the operation
    on them.
    */

    private Dictionary<int, ComponentBehavior> selectedGameObjects;

    private SelectionAreaManager selectionArea;

    

    private void Start()
    {
        selectedGameObjects = new Dictionary<int, ComponentBehavior>();
        
        selectionArea = Instantiate(((GameObject)Resources.Load("SelectionArea"))).GetComponent<SelectionAreaManager>();
    }

    public bool onGoingMultiSelect {get; private set;} = false;

    public void StartMultiSelect(Vector2 anchor)
    {
        onGoingMultiSelect = true;
        selectionArea.Enable();
        selectionArea.SetAnchorPoint(anchor);
    }

    public void AdjuctMultiSelect(Vector2 point)
    {
        selectionArea.Adjust(point);
    }

    public List<ComponentBehavior> EndMultiSelect()
    {
        var t = selectionArea.GetSelection();
        selectionArea.Disable();
        onGoingMultiSelect = false;
        return t;
    }

    public void InvertComponent(ComponentBehavior component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) RemoveComponent(component);
        else
        {
            selectedGameObjects.Add(component.GetInstanceID(), component);
            component.SetSelectedVisible(true);
        }
    }

    public void AddComponent(ComponentBehavior component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) return;
        else
        {
            selectedGameObjects.Add(component.GetInstanceID(), component);
            component.SetSelectedVisible(true);
        }
    }

    public void RemoveComponent(ComponentBehavior component)
    {
        selectedGameObjects.Remove(component.GetInstanceID());
        component.SetSelectedVisible(false);
    }

    public void InvertComponents(List<ComponentBehavior> components)
    {
        foreach (var component in components)
        {
            InvertComponent(component);
        }
    }

    public void AddComponents(List<ComponentBehavior> components)
    {
        foreach (var component in components)
        {
            AddComponent(component);
        }
    }

    public void RemoveComponents(List<ComponentBehavior> components)
    {
        foreach (var component in components)
        {
            RemoveComponent(component);
        }
    }

    public void Clear()
    {
        RemoveComponents(new List<ComponentBehavior>(selectedGameObjects.Values));
        selectedGameObjects.Clear(); // just to make sure
    }

    public List<ComponentBehavior> GetSelectedComponents(bool includeDisabled = false)
    {
        if (includeDisabled) return new List<ComponentBehavior>(selectedGameObjects.Values); 
        return selectedGameObjects.Values.Where(c => c.IsEnabled()).ToList();
    }

    public bool IsSelected(ComponentBehavior c) {
        return selectedGameObjects.ContainsKey(c.GetInstanceID());
    }
}
