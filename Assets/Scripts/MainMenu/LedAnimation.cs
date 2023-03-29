using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedAnimation : MonoBehaviour
{
    PointLight led;
    private void Awake()
    {
        led = GameObject.Find("led").GetComponentInChildren<PointLight>();
        // led.lerpSpeed /= 2;
        Alternate();
    }
    private void Alternate()
    {
        led.SetIntensity(Random.Range(0, Led.MAX_INTENSITY));
        Invoke("Alternate", Random.Range(0.5f,2f));
    }
}
