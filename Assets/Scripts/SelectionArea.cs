using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArea : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        transform.localScale = Vector2.zero;
        transform.position = Vector2.zero;
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

        Collider2D[] collidedObjectsArray = Physics2D.OverlapAreaAll(sprite.bounds.min, sprite.bounds.max);
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
