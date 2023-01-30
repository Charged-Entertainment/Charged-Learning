using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComponentBehavior : MonoBehaviour
{
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

    public void Destroy() {
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

    public Bounds GetBounds(){

        Bounds bounds = transform.GetComponent<Collider2D>().bounds;
        return bounds;
    }

    public bool IsEnabled() {
        return gameObject.activeSelf;
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    // Events
    private void OnMouseDown()
    {
        if (ComponentManager.componentMouseDown != null)
            ComponentManager.componentMouseDown.Invoke(this);
    }

    private void OnMouseUp()
    {
        if (ComponentManager.componentMouseUp != null)
            ComponentManager.componentMouseUp.Invoke(this);
    }

    private void OnMouseDrag()
    {
        if (ComponentManager.componentDragged != null)
            ComponentManager.componentDragged.Invoke(this);
    }

    private void OnMouseEnter()
    {
        if (ComponentManager.componentMouseEntered != null)
            ComponentManager.componentMouseEntered.Invoke(this);
    }

    private void OnMouseExit()
    {
        if (ComponentManager.componentMouseExited != null)
            ComponentManager.componentMouseExited.Invoke(this);
    }

}
