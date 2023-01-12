using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    /*
    Any operations that should be performed on selected objects will first query
    this class for whatever available selected objects it has, then perform the operation
    on them.
    */

    public Dictionary<int, ComponentBehavior> selectedGameObjects { get; private set; }

    private SelectionArea selectionArea;
    private Clipboard clipboard;

    private ComponentManager cm;

    private byte numberOfHovers = 0;

    void Awake()
    {
        selectedGameObjects = new Dictionary<int, ComponentBehavior>();
    }

    private void Start()
    {
        selectionArea = GameObject.Find("SelectionArea").GetComponent<SelectionArea>();
        cm = GameObject.Find("ComponentManager").GetComponent<ComponentManager>();
        clipboard = GameObject.Find("Clipboard").GetComponent<Clipboard>();

        selectionArea.Disable();
        cm.componentMouseEntered += (ComponentBehavior c) => { numberOfHovers++; };
        cm.componentMouseExited += (ComponentBehavior c) => { numberOfHovers--; };
        cm.componentMouseDown += (ComponentBehavior c) =>
        {
            if (!Input.GetKey(KeyCode.LeftShift)) Clear();
            AddComponent(c);
        };
    }

    bool onGoingMultiSelect = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && numberOfHovers == 0) //0=Left click
        {
            if (!Input.GetKey(KeyCode.LeftShift)) Clear();
            StartMultiSelect();
        }

        if (Input.GetMouseButton(0) && onGoingMultiSelect)
        {
            MultiSelect();
        }

        if (Input.GetMouseButtonUp(0) && onGoingMultiSelect)
        {
            var selected = EndMultiSelect();

            if (!Input.GetKey(KeyCode.LeftShift)) Clear();
            AddComponents(selected);
        }
    }

    void StartMultiSelect()
    {
        onGoingMultiSelect = true;
        selectionArea.Enable();
        selectionArea.SetAnchorPoint(Utils.GetMouseWorldPosition());
    }

    void MultiSelect()
    {
        selectionArea.Adjust(Utils.GetMouseWorldPosition());
    }

    List<ComponentBehavior> EndMultiSelect()
    {
        var t = selectionArea.GetSelection();
        selectionArea.Disable();
        onGoingMultiSelect = false;
        return t;
    }

    public void AddComponent(ComponentBehavior component)
    {
        if (selectedGameObjects.ContainsKey(component.GetInstanceID())) RemoveComponent(component);
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
}
