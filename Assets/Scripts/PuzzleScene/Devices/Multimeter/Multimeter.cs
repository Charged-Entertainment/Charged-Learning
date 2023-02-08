using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multimeter : Device
{
    public override void ChangeMode(DeviceMode newMode)
    {
        deviceMode?.OnExit();
        newMode.OnEnter();
        deviceMode = newMode;
    }
}
