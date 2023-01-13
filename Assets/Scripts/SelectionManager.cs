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

    private Dictionary<int, ComponentBehavior> selectedGameObjects;

    private SelectionArea selectionArea;
    private Clipboard clipboard;

    private ComponentManager cm;

    private byte numberOfHovers = 0;

    void Awake()
    {
        selectedGameObjects = new Dictionary<int, ComponentBehavior>();
    }

    Vector3 mouseDownStartPosition;
    bool handleOnMouseUp = false;
    bool shiftWasClickedOnMouseDown = false;

    private void Start()
    {
        selectionArea = GameObject.Find("SelectionArea").GetComponent<SelectionArea>();
        cm = GameObject.Find("ComponentManager").GetComponent<ComponentManager>();
        clipboard = GameObject.Find("Clipboard").GetComponent<Clipboard>();

        selectionArea.Disable();
        cm.componentMouseEntered += (ComponentBehavior c) => { numberOfHovers++; };
        cm.componentMouseExited += (ComponentBehavior c) => { numberOfHovers--; };

        // This should probably be moved somewhere else
        cm.componentMouseDown += (ComponentBehavior c) =>
        {
            bool shiftClicked = Input.GetKey(KeyCode.LeftShift);
            bool isSelected = selectedGameObjects.ContainsKey(c.GetInstanceID());

            mouseDownStartPosition = Utils.GetMouseWorldPosition();
            if (shiftClicked)
            {
                if (!isSelected) AddComponent(c);
                else { handleOnMouseUp = true; shiftWasClickedOnMouseDown = true; }
            }
            else
            {
                if (!isSelected) { Clear(); AddComponent(c); }
                else if (selectedGameObjects.Count > 1) { handleOnMouseUp = true; }
            }
        };

        cm.componentMouseUp += (ComponentBehavior c) =>
        {
            if (handleOnMouseUp && mouseDownStartPosition == Utils.GetMouseWorldPosition())
            {
                if (shiftWasClickedOnMouseDown) { InvertComponent(c); }
                else { Clear(); AddComponent(c); }
            }
            handleOnMouseUp = false;
            shiftWasClickedOnMouseDown = false;
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
            InvertComponents(selected);
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

    public List<ComponentBehavior> GetSelectedComponents()
    {
        return new List<ComponentBehavior>(selectedGameObjects.Values);
    }
}
