using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArea : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetAnchorPoint(Vector2 point)
    {
        transform.position = point;
    }

    public void Adjust(Vector2 currentPosition)
    {
        // transform.position -= sprite.bounds.extents;
        transform.localScale = currentPosition - (Vector2)transform.position;
        // transform.position += sprite.bounds.extents;
    }

    public List<ComponentBehavior> GetSelection()
    {
        List<ComponentBehavior> matches = new List<ComponentBehavior>();

        Collider2D[] collidedObjectsArray = Physics2D.OverlapAreaAll(transform.position, transform.localScale);
        foreach (var collidedObject in collidedObjectsArray)
        {
            ComponentBehavior componentObject = collidedObject.GetComponent<ComponentBehavior>();
            if (componentObject)
            {
                matches.Add(componentObject);
            }
        }

        return matches;
    }
}
