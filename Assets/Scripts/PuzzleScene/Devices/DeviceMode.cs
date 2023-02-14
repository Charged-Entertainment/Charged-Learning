using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DeviceMode
{
    public void OnEnter(Device device);
    public void OnExit();
}