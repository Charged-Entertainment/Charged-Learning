using System;
using UnityEngine;
using GameManagement;
using System.Collections.Generic;

public partial class EditorBehaviour : MonoBehaviour, ContextMenuObject
{
    List<ContextMenuElement> contextMenuElements;

    static public Action<EditorBehaviour> mouseEntered;
    static public Action<EditorBehaviour> mouseExited;
    static public Action<EditorBehaviour> mouseDown;
    static public Action<EditorBehaviour> mouseUp;
    static public Action<EditorBehaviour> created;
    static public Action<EditorBehaviour> destroyed;
    static public Action<EditorBehaviour> dragged;
    static public Action<EditorBehaviour> moved;

    private void Start() {
        contextMenuElements = new List<ContextMenuElement>(){
            new ContextMenuElement("Flip Horizontally", "Shift+H", FlipH),
            new ContextMenuElement("Flip Vertically", "Shift+V", FlipV),
            new ContextMenuElement("Rotate right", "Ctrl+R", () => Rotate(-90)),
            new ContextMenuElement("Rotate left", "Ctrl+Q", () => Rotate(90)),
            new ContextMenuElement("Delete", "Delete", Destroy),
        };
    }

    public List<ContextMenuElement> GetContextMenuElements(){
        return contextMenuElements;
    }



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

    virtual protected void Awake()
    {
        created?.Invoke(this);
    }

    virtual public bool IsEnabled()
    {
        return gameObject.activeSelf;
    }

    virtual public void Enable()
    {
        gameObject.SetActive(true);
    }

    virtual public void Disable()
    {
        gameObject.SetActive(false);
    }

    virtual public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    virtual protected void OnMouseDown()
    {
        mouseDown?.Invoke(this);
    }

    virtual protected void OnMouseUp()
    {
        mouseUp?.Invoke(this);
    }

    virtual protected void OnMouseDrag()
    {
        dragged?.Invoke(this);
    }

    virtual protected void OnMouseEnter()
    {
        mouseEntered?.Invoke(this);
    }

    virtual protected void OnMouseExit()
    {
        mouseExited?.Invoke(this);
    }

    virtual protected void OnDestroy()
    {
        destroyed?.Invoke(this);
    }
}
