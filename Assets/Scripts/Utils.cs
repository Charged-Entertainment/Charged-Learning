using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 GetMouseWorldPosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    //TODO: generic version
    // public static Collider2D PhysicsOverlapAreaAll<T>(Vector2 PointA, Vector2 PointB)
}
