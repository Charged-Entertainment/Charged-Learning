using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyKnob : MonoBehaviour
{
    private int minAngle = 0, maxAngle = 180;
    private void OnMouseDrag()
    {
        if (isActiveAndEnabled)
        {
            var diff = Utils.GetMouseWorldPosition() - transform.position;
            diff.Normalize();
            float rotationZ = 180 - Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            if (rotationZ > 180) rotationZ -= 360;
            if (rotationZ < 0) rotationZ = rotationZ > -90 ? minAngle : maxAngle;

            rotationZ = Mathf.Clamp(rotationZ, minAngle, maxAngle);
            transform.localRotation = Quaternion.Euler(0, 0, -1 * rotationZ);
        }
    }
    public float Value
    {
        get
        {
            return -transform.localRotation.z;
        }
    }
}
