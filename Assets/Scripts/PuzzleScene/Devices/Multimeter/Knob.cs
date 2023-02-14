using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    [SerializeField]private Multimeter multiMeter;
    private Vector2 turn;
    private int currentRotationAngle = 18, numberOfModes, degreePerMode;

    private void Start() {
        multiMeter = transform.parent.parent.GetComponent<Multimeter>();
        numberOfModes = 20;
        degreePerMode = 360/numberOfModes;
    }
    private void Update() {
    }

    ///<summary>Calculates the angle that the knob should be rotated at 
    ///when the mouse is dragging it.
    ///</summary>
    private void OnMouseDrag() {
        var diff = Utils.GetMouseWorldPosition() - transform.position;
        diff.Normalize();
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        var newAngle = Mathf.RoundToInt(rotationZ/degreePerMode)*degreePerMode;
        if(newAngle < 0)newAngle+=360;
        CheckAngleChange(newAngle);
        currentRotationAngle = newAngle;
        transform.localRotation = Quaternion.Euler(0,0, currentRotationAngle);        
    }

    private void CheckAngleChange(int newAngle){
        if(newAngle == currentRotationAngle) return;
        multiMeter.ChangeMode(GetAngleMode(newAngle));

    }
    
    ///<summary>Takes an angle and return the appropriate mode for it.</summary>
    private DeviceMode GetAngleMode(int angle){
        if(angle==0)
            return new OffMode();
        if(angle is > 0 and <= 18*5)
            return new VoltageMode(false, "200");
        if(angle is > 18*5 and <= 18*11)
            return new ResistanceMode("200");
        if(angle is > 18*11 and <= 18*14)
            return new CurrentMode(false, "200");
        if(angle is > 18*14 and <= 18*17)
            return new CurrentMode(true, "200");
        return new VoltageMode(true, "200");
    }

    private void OnMouseDown() {
    }
}
