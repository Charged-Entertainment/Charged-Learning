using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PointLight : MonoBehaviour
{
    [SerializeField] private new Light2D light;
    [SerializeField] public float lerpSpeed = 3;
    [SerializeField] private float targetIntensity = 0;

    private void Awake()
    {
        light = gameObject.GetComponent<Light2D>();
    }

    private void Update()
    {
        if (targetIntensity != light.intensity)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, Time.deltaTime * lerpSpeed);
        }
    }

    /// <summary>
    /// Lerp (animate) the light intensity to the given value. To set the intensity immediately use SetIntensityImmediate instead. 
    /// </summary>
    public void SetIntensity(float intensity)
    {
        targetIntensity = intensity;
    }

    /// <summary>
    /// Set the light intensity to the given value immediately. The intensity will lerp back to its old value by default.
    /// </summary>
    public void SetIntensityImmediate(float intensity, bool lerpBack = true)
    {
        light.intensity = intensity;
        if (!lerpBack) targetIntensity = intensity;
    }
}
