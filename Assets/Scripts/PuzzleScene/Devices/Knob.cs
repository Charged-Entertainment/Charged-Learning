using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    private Multimeter multiMeter;
    private Vector2 turn;

    private void Start() {
        // multiMeter = transform.parent.GetComponent<Multimeter>();
        // turn = Vector2.zero;
    }
    private void Update() {
    }
    private void OnMouseDrag() {
        var diff = Utils.GetMouseWorldPosition();
        diff.Normalize();
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, rotationZ);

    }

    private void OnMouseDown() {
        Debug.Log("Mouse Down");
    }
}
