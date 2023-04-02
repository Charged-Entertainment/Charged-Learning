using System;
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

    static private Dictionary<int, EditorBehaviour> selectedGameObjects;

    static private SelectionArea selectionArea = null;

    private SelectionController selectionController;

    public static Action<EditorBehaviour> objectAdded, objectRemoved;
    public static Action cleared, selectionChanged;

    private new void Awake()
    {
        base.Awake();
        selectedGameObjects = new Dictionary<int, EditorBehaviour>();
        selectionController = gameObject.AddComponent<SelectionController>();
    }

    private void OnEnable()
    {
        OnDisable();
        GameMode.changed += HandleGameModeChange;
        InteractionMode.changed += HandleInteractionModeChange;
        EditorBehaviour.destroyed += HandleComponentDestroyed;
    }

    private void OnDisable()
    {
        GameMode.changed -= HandleGameModeChange;
        InteractionMode.changed -= HandleInteractionModeChange;
        EditorBehaviour.destroyed -= HandleComponentDestroyed;
    }

    private void HandleComponentDestroyed(EditorBehaviour c)
    {
        int key = c.GetInstanceID();
        if (selectedGameObjects.ContainsKey(key)) selectedGameObjects.Remove(key);
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
        if (selectionArea == null) selectionArea = Instantiate(((GameObject)Resources.Load("Prefabs/SelectionArea"))).AddComponent<SelectionArea>();
        selectionArea.SetAnchorPoint(anchor);
    }

    static public void AdjuctMultiSelect(Vector2 point)
    {
        selectionArea.Adjust(point);
    }

    static public List<EditorBehaviour> EndMultiSelect()
    {
        var t = selectionArea.GetSelection();
        GameObject.Destroy(selectionArea.gameObject);
        selectionArea = null;
        OnGoingMultiSelect = false;
        return t;
    }

    static public void InvertComponent(EditorBehaviour component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) RemoveComponent(component);
        else AddComponent(component);

    }

    static public void AddComponent(EditorBehaviour component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) return;
        else
        {
            selectedGameObjects.Add(component.GetInstanceID(), component);
            component.gameObject.AddComponent<SelectedObjectOverlay>();
            objectAdded?.Invoke(component);
            selectionChanged?.Invoke();
        }
    }

    static public void RemoveComponent(EditorBehaviour component)
    {
        selectedGameObjects.Remove(component.GetInstanceID());
        GameObject.Destroy(component.gameObject.GetComponent<SelectedObjectOverlay>());
        objectRemoved?.Invoke(component);
        selectionChanged?.Invoke();
    }

    static public void InvertComponents(IList<EditorBehaviour> components)
    {
        foreach (var component in components)
        {
            InvertComponent(component);
        }
    }

    static public void AddComponents(IList<EditorBehaviour> components)
    {
        foreach (var component in components)
        {
            AddComponent(component);
        }
    }

    static public void RemoveComponents(IList<EditorBehaviour> components)
    {
        foreach (var component in components)
        {
            RemoveComponent(component);
        }
    }

    static public void Clear()
    {
        RemoveComponents(selectedGameObjects.Values.ToArray());
        selectedGameObjects.Clear(); // just to make sure
        cleared?.Invoke();
        selectionChanged?.Invoke();
    }

    static public List<T> GetSelectedComponents<T>(bool includeDisabled = false) where T : EditorBehaviour
    {
        List<T> res = new List<T>();
        foreach (var item in selectedGameObjects) {
            var t = item.Value.GetComponent<T>();
            if (t != null) res.Add(t);
        }
        if (includeDisabled) return res;
        return res.Where(c => c.IsEnabled()).ToList();
    }

    static public bool IsSelected(EditorBehaviour c)
    {
        return selectedGameObjects.ContainsKey(c.GetInstanceID());
    }
}
