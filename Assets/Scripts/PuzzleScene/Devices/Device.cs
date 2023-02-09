using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour
{
    protected DeviceMode deviceMode;
    [SerializeField]protected Probe negativeProbe;
    [SerializeField]protected Probe positiveProbe;

    void Start()
    {
        negativeProbe = transform.GetChild(0).GetComponent<Probe>();
        positiveProbe = transform.GetChild(1).GetComponent<Probe>();
        Debug.Log($"NegativeProbe: {negativeProbe}");
        Debug.Log($"PositiveProbe: {positiveProbe}");
    }

    public abstract void ChangeMode(DeviceMode newMode);
}
