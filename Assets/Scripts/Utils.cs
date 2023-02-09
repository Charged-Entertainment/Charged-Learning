using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Utils : MonoBehaviour
{
    public static Vector3 GetMouseWorldPosition()
    {
        return UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //TODO: generic version
    // public static Collider2D PhysicsOverlapAreaAll<T>(Vector2 PointA, Vector2 PointB)

    public static Bounds GetBoundsOfComponentsArray(ComponentBehavior[] components)
    {

        if(components.Length == 0)
            return new Bounds();   
            
        Bounds bounds = new Bounds(components[0].transform.position, Vector3.zero);
        foreach(var cmp in components){
            bounds.Encapsulate(cmp.GetBounds());
        }
        return bounds;
    }

    public static Terminal[] GetTerminals(ComponentBehavior c) {
        return c.gameObject.GetComponentsInChildren<Terminal>(true);
    }
}
