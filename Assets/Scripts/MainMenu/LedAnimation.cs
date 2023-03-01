using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedAnimation : MonoBehaviour
{
    Led led;
    private void Awake()
    {
        led = GameObject.Find("led").GetComponent<Led>();
        // led.lerpSpeed /= 2;
        Alternate();
    }
    private void Alternate()
    {
        led.SetIntensity(Random.Range(Led.MIN_INTENSITY, Led.DANGEROUS_MAX_INTENSITY));
        Invoke("Alternate", Random.Range(0.5f,2f));
    }
}
