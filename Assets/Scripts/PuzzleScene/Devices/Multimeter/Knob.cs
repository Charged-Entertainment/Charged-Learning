using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    [SerializeField] private Multimeter multiMeter;
    private Vector2 turn;
    // private int currentRotationAngle = 18, numberOfModes, degreePerMode;
    private int currentRotationAngle = 0, degreePerMode = 30;
    private int minAngle = -60, maxAngle = 60;

    private void Start()
    {
        multiMeter = transform.parent.parent.GetComponent<Multimeter>();
    }

    ///<summary>Calculates the angle that the knob should be rotated at 
    ///when the mouse is dragging it.
    ///</summary>
    private void OnMouseDrag()
    {
        if (isActiveAndEnabled)
        {
            var diff = Utils.GetMouseWorldPosition() - transform.position;
            diff.Normalize();
            float rotationZ = 90 - Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            if (rotationZ > 180) rotationZ -= 360;
            rotationZ = Mathf.RoundToInt(rotationZ / degreePerMode) * degreePerMode;
            rotationZ = Mathf.Clamp(rotationZ, minAngle, maxAngle);
            
            CheckAngleChange((int)rotationZ);
            currentRotationAngle = (int)rotationZ;
            transform.localRotation = Quaternion.Euler(0, 0, -1 * rotationZ);
        }
    }

    private void CheckAngleChange(int newAngle)
    {
        if (newAngle == currentRotationAngle) return;
        multiMeter.ChangeMode(GetAngleMode(newAngle));
    }

    ///<summary>Takes an angle and return the appropriate mode for it.</summary>
    private DeviceMode GetAngleMode(int angle)
    {
        if (angle == -60)
            return new ResistanceMode("200");
        if (angle is -30)
            return new VoltageMode(false, "200");
        if (angle is 0)
            return new OffMode();
        if (angle is 30)
            return new CurrentMode(false, "200");
        // if(angle is > 60); not implemented yet

        // else
        return new LockedMode();
    }

    public static void SetEnabled(bool enabled)
    {
        var knob = GameObject.Find("multimeter-knob");
        if (knob != null) knob.GetComponent<Knob>().enabled = enabled;
        else Debug.Log("multimeter-knob doesn't exist");
    }
}
