using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnapping : MonoBehaviour
{

    private void OnMouseDrag() {
        Debug.Log("Componenet getting dragged");
        int tile_width =5, tile_height = 1;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Round(mouseWorldPos.x / tile_width)*tile_width, Mathf.Round(mouseWorldPos.y/tile_height)*tile_height);
    }


}
