using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    public DeviceMode DeviceMode{get; protected set;}
    public abstract void ChangeMode(DeviceMode newMode);
}

