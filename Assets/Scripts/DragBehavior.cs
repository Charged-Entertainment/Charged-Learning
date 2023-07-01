using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    private Vector3 dragStart;
    private void OnMouseDown() {
        dragStart = Utils.GetMouseWorldPosition();
    }

    private void OnMouseDrag() {
        transform.position += (Utils.GetMouseWorldPosition() - dragStart);
        dragStart = Utils.GetMouseWorldPosition();
    }
}
