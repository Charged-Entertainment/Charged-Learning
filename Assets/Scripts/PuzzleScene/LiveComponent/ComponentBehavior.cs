using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComponentBehavior : MonoBehaviour
{
    public Components.LevelComponent levelComponent;

    private void Start()
    {
        gameObject.AddComponent<ObjectSnapping>();
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

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }

    public void SetSelectedVisible(bool selected)
    {
        if (selected)
        {
            foreach (Transform t in transform)
            {
                t.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            foreach (Transform t in transform)
            {
                t.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

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

    // Events
    private void OnMouseDown()
    {
        if (ComponentManager.mouseDown != null)
            ComponentManager.mouseDown.Invoke(this);
    }

    private void OnMouseUp()
    {
        if (ComponentManager.mouseUp != null)
            ComponentManager.mouseUp.Invoke(this);
    }

    private void OnMouseDrag()
    {
        if (ComponentManager.dragged != null)
            ComponentManager.dragged.Invoke(this);
    }

    private void OnMouseEnter()
    {
        if (ComponentManager.mouseEntered != null)
            ComponentManager.mouseEntered.Invoke(this);
    }

    private void OnMouseExit()
    {
        if (ComponentManager.mouseExited != null)
            ComponentManager.mouseExited.Invoke(this);
    }

}
