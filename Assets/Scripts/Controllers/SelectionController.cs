using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class SelectionController : Controller<SelectionController>
    {
        byte numberOfHovers = 0;

        Vector3 mouseDownStartPosition;
        bool handleOnMouseUp = false;
        bool shiftWasClickedOnMouseDown = false;

        void Start()
        {
            ComponentManager.componentMouseEntered += (ComponentBehavior c) => { numberOfHovers++; };
            ComponentManager.componentMouseExited += (ComponentBehavior c) => { numberOfHovers--; };

            ComponentManager.componentMouseDown += (ComponentBehavior c) =>
            {
                bool shiftClicked = Input.GetKey(KeyCode.LeftShift);
                bool isSelected = Selection.IsSelected(c);

                mouseDownStartPosition = Utils.GetMouseWorldPosition();
                if (shiftClicked)
                {
                    if (!isSelected) Selection.AddComponent(c);
                    else { handleOnMouseUp = true; shiftWasClickedOnMouseDown = true; }
                }
                else
                {
                    if (!isSelected) { Selection.Clear(); Selection.AddComponent(c); }
                    else if (Selection.GetSelectedComponents().Count > 1) { handleOnMouseUp = true; }
                }
            };

            ComponentManager.componentMouseUp += (ComponentBehavior c) =>
            {
                if (handleOnMouseUp && mouseDownStartPosition == Utils.GetMouseWorldPosition())
                {
                    if (shiftWasClickedOnMouseDown) { Selection.InvertComponent(c); }
                    else { Selection.Clear(); Selection.AddComponent(c); }
                }
                handleOnMouseUp = false;
                shiftWasClickedOnMouseDown = false;
            };
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && numberOfHovers == 0) //0=Left click
            {
                if (!Input.GetKey(KeyCode.LeftShift)) Selection.Clear();
                Selection.StartMultiSelect(Utils.GetMouseWorldPosition());
            }

            if (Input.GetMouseButton(0) && Selection.OnGoingMultiSelect)
            {
                Selection.AdjuctMultiSelect(Utils.GetMouseWorldPosition());
            }

            if (Input.GetMouseButtonUp(0) && Selection.OnGoingMultiSelect)
            {
                var selected = Selection.EndMultiSelect();

                if (!Input.GetKey(KeyCode.LeftShift)) Selection.Clear();
                Selection.InvertComponents(selected);
            }
        }
    }
}