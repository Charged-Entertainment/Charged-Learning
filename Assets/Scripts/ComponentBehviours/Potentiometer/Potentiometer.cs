using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potentiometer : MonoBehaviour
{
    Transform point, ellipse;
    static readonly float ellipseScaleFactor = 0.7f;
    float width, height; 

    /// <summary>
    /// The only parameter needed to control the position of the point. It should be in the range of (0:2*width).
    /// </summary>
    [SerializeField] float c { get { return __c; } set { __c = Mathf.Clamp(value, 0, 2 * width); } }
    float __c = 0;

    private void Awake()
    {
        ellipse = transform.Find("ellipse");
        point = ellipse.gameObject.transform.Find("point");
        width = ellipse.GetComponent<SpriteRenderer>().bounds.extents.x * ellipseScaleFactor;
        height = ellipse.GetComponent<SpriteRenderer>().bounds.extents.y * ellipseScaleFactor;
    }

    private void Update()
    {
        point.transform.position = new Vector3(c - width, g(c), transform.position.z) + ellipse.position;
    }

    float mouseStartPositionX = 0f;
    private void OnMouseDown()
    {
        mouseStartPositionX = Utils.GetMouseWorldPosition().x;
    }

    private void OnMouseDrag()
    {
        var mousePos = Utils.GetMouseWorldPosition().x;
        if (mousePos != mouseStartPositionX) c = (mouseStartPositionX - mousePos);
    }

    // x from 0 to 2*width
    private float g(float x)
    {
        // The upper positive half of an ellipse, shifted to start from 0 to 2*width
        // g(x) = sqrt( h^2 - ((height/width)*(x - width))^2 )
        return Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow((height / width) * (x - width), 2));
    }


    /// <summary>
    /// A ratio (0:1) indicating how far along is the potentiometer turned. A ratio of 1 means the potentiometer should be at its maximum value.
    /// </summary>
    public float Value { get { return c / width * 0.5f; } }
}
