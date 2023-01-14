using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : Controller
{
    byte numberOfHovers = 0;

    Vector3 mouseDownStartPosition;
    bool handleOnMouseUp = false;
    bool shiftWasClickedOnMouseDown = false;

    SelectionManager sm;

    void Start()
    {
        sm = mainManager.selectionManager;

        var cm = mainManager.componentManager;
        cm.componentMouseEntered += (ComponentBehavior c) => { numberOfHovers++; };
        cm.componentMouseExited += (ComponentBehavior c) => { numberOfHovers--; };

        cm.componentMouseDown += (ComponentBehavior c) =>
        {
            bool shiftClicked = Input.GetKey(KeyCode.LeftShift);
            bool isSelected = sm.IsSelected(c);

            mouseDownStartPosition = Utils.GetMouseWorldPosition();
            if (shiftClicked)
            {
                if (!isSelected) sm.AddComponent(c);
                else { handleOnMouseUp = true; shiftWasClickedOnMouseDown = true; }
            }
            else
            {
                if (!isSelected) { sm.Clear(); sm.AddComponent(c); }
                else if (sm.GetSelectedComponents().Count > 1) { handleOnMouseUp = true; }
            }
        };

        cm.componentMouseUp += (ComponentBehavior c) =>
        {
            if (handleOnMouseUp && mouseDownStartPosition == Utils.GetMouseWorldPosition())
            {
                if (shiftWasClickedOnMouseDown) { sm.InvertComponent(c); }
                else { sm.Clear(); sm.AddComponent(c); }
            }
            handleOnMouseUp = false;
            shiftWasClickedOnMouseDown = false;
        };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && numberOfHovers == 0) //0=Left click
        {
            if (!Input.GetKey(KeyCode.LeftShift)) sm.Clear();
            sm.StartMultiSelect(Utils.GetMouseWorldPosition());
        }

        if (Input.GetMouseButton(0) && sm.onGoingMultiSelect)
        {
            sm.AdjuctMultiSelect(Utils.GetMouseWorldPosition());
        }

        if (Input.GetMouseButtonUp(0) && sm.onGoingMultiSelect)
        {
            var selected = sm.EndMultiSelect();

            if (!Input.GetKey(KeyCode.LeftShift)) sm.Clear();
            sm.InvertComponents(selected);
        }
    }
}
