using System;
using UnityEngine;
using GameManagement;

public partial class EditorBehaviour : MonoBehaviour
{
    static public Action<EditorBehaviour> mouseEntered;
    static public Action<EditorBehaviour> mouseExited;
    static public Action<EditorBehaviour> mouseDown;
    static public Action<EditorBehaviour> mouseUp;

    private class EditorBehaviourController : Singleton<Controller>
    {
        private static DragController dragController;
        private static TransformationController transformationController;

        new void Awake()
        {
            base.Awake();
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
    }

    static public Action<EditorBehaviour> dragged;
    static public Action<EditorBehaviour> moved;
    public void FlipH()
    {
        var current_scale = transform.localScale;
        transform.localScale = new Vector3(-current_scale.x, current_scale.y, current_scale.z);
    }

    public void FlipV()
    {
        var current_scale = transform.localScale;
        transform.localScale = new Vector3(current_scale.x, -current_scale.y, current_scale.z);
    }

    public void Move(Vector3 value, bool isOffset = true)
    {
        if (isOffset)
        {
            transform.position += value;
        }
        else transform.position = value;
        EditorBehaviour.moved?.Invoke(this);
    }

    public void Rotate(float value, bool isOffset = true)
    {
        var x = transform.rotation.eulerAngles.x;
        var y = transform.rotation.eulerAngles.y;

        if (isOffset)
        {
            transform.Rotate(x, y, value);
        }
        else transform.rotation = Quaternion.Euler(x, y, value);
    }

    public Bounds GetBounds()
    {
        Bounds bounds = transform.GetComponent<Collider2D>().bounds;
        return bounds;
    }

    public bool IsEnabled()
    {
        return gameObject.activeSelf;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        mouseDown?.Invoke(this);
    }

    private void OnMouseUp()
    {
        mouseUp?.Invoke(this);
    }

    private void OnMouseDrag()
    {
        dragged?.Invoke(this);
    }

    private void OnMouseEnter()
    {
        mouseEntered?.Invoke(this);
    }

    private void OnMouseExit()
    {
        mouseExited?.Invoke(this);
    }
}
