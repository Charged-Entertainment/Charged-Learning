using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CLGraphing;
public class GraphingToolDisplay : MonoBehaviour
{
    public GameObject container { get; private set; }
    public float width { get; private set; }
    public float height { get; private set; }
    private void Awake()
    {
        var extents = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        width = extents.x *2;
        height = extents.y *2;
        container = transform.Find("GraphContainer").gameObject;
    }
    public void TurnOff() { enabled = false; }
}
