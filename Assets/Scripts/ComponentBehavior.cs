using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBehavior : MonoBehaviour
{

    public void FlipH()
    {
        var current_scale = transform.localScale;
        transform.localScale = new Vector3(-current_scale.x, current_scale.y, current_scale.z);
    }

    public void FlipV()
    {
        var current_scale = transform.localScale;
        transform.localScale = new Vector3(current_scale.x, -current_scale.y, current_scale.z);
    }

    public void Move(Vector3 value, bool isOffset = true)
    {
        if (isOffset)
        {
            transform.position += value;
        }
        else transform.position = value;
    }

    public void Rotate(float value, bool isOffset = true)
    {
        var x = transform.rotation.eulerAngles.x;
        var y = transform.rotation.eulerAngles.y;

        if (isOffset)
        {
            transform.Rotate(x, y, value);
        }
        else transform.rotation = Quaternion.Euler(x, y, value);
    }

    public void SetSelectedVisible(bool selected)
    {
        if (selected)
        {

            foreach (Transform t in transform)
            {
                t.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            foreach (Transform t in transform)
            {
                t.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

    }
}
