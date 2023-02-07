using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Selection : Singleton<Selection>
{
    private class SelectionController : Controller
    {
        byte numberOfHovers = 0;
        Vector3 mouseDownStartPosition;
        bool handleOnMouseUp = false;
        bool shiftWasClickedOnMouseDown = false;

        private void Awake() {
            // Never disabled.
            ComponentManager.mouseEntered += HandleMouseEntered;
            ComponentManager.mouseExited += HandleMouseExited;
        }

        void OnEnable()
        {
            OnDisable();
            ComponentManager.mouseDown += HandleMouseDown;
            ComponentManager.mouseUp += HandleMouseUp;
        }

        void OnDisable()
        {
            ComponentManager.mouseDown -= HandleMouseDown;
            ComponentManager.mouseUp -= HandleMouseUp;
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

        void HandleMouseEntered(ComponentBehavior c) { numberOfHovers++; }
        void HandleMouseExited(ComponentBehavior c) { numberOfHovers--; }
        void HandleMouseDown(ComponentBehavior c)
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
        }
        void HandleMouseUp(ComponentBehavior c)
        {
            if (handleOnMouseUp && mouseDownStartPosition == Utils.GetMouseWorldPosition())
            {
                if (shiftWasClickedOnMouseDown) { Selection.InvertComponent(c); }
                else { Selection.Clear(); Selection.AddComponent(c); }
            }
            handleOnMouseUp = false;
            shiftWasClickedOnMouseDown = false;
        }
    }
}