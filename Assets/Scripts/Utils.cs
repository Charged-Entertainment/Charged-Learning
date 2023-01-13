using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //TODO: generic version
    // public static Collider2D PhysicsOverlapAreaAll<T>(Vector2 PointA, Vector2 PointB)

    public static Bounds GetBoundsOfComponentBehaviorArray(ComponentBehavior[] components)
    {

        if(components.Length == 0)
            return new Bounds();   
            
        Bounds bounds = new Bounds(components[0].transform.position, Vector3.zero);
        foreach(var cmp in components){
            bounds.Encapsulate(cmp.GetBounds());
        }
        return bounds;
    }
}
