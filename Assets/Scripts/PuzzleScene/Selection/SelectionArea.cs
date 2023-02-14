using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Selection : Singleton<Selection>
{
    private class SelectionArea: MonoBehaviour
    {
        public void SetAnchorPoint(Vector2 point)
        {
            transform.position = point;
        }

        public void Adjust(Vector2 currentPosition)
        {
            transform.localScale = currentPosition - (Vector2)transform.position;
        }

        public List<ComponentBehavior> GetSelection()
        {
            List<ComponentBehavior> matches = new List<ComponentBehavior>();

            Collider2D[] collidedObjectsArray = Physics2D.OverlapAreaAll(transform.position, transform.position + transform.localScale);
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

}
