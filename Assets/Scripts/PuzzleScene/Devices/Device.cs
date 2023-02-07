using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    private DeviceMode deviceMode;
    [SerializeField]protected Probe negativeProbe;
    [SerializeField]protected Probe positiveProbe;
    //TODO: private DeviceDisplay 

    void Start()
    {
        negativeProbe = transform.GetChild(0).GetComponent<Probe>();
        positiveProbe = transform.GetChild(1).GetComponent<Probe>();
        Debug.Log($"NegativeProbe: {negativeProbe}");
        Debug.Log($"PositiveProbe: {positiveProbe}");
    }

    void Update()
    {
        deviceMode?.Update();
    }

    public void ChangeMode(DeviceMode newMode){
        deviceMode = newMode;
    }
}

