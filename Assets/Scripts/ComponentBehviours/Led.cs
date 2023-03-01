using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Led : MonoBehaviour
{
    public readonly static float MIN_INTENSITY = 0;
    public readonly static float SAFE_MAX_INTENSITY = 10;
    public readonly static float DANGEROUS_MAX_INTENSITY = 15;

    [SerializeField] private new Light2D light;
    [SerializeField] public float lerpSpeed = 3;
    [SerializeField] private float targetIntensity = 0;
    void Start()
    {
        light = gameObject.GetComponent<Light2D>();
        light.intensity = 0;
    }
    private void Update()
    {
        if (targetIntensity != light.intensity)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, Time.deltaTime * lerpSpeed);
        }
    }
    public void SetIntensity(float intensity)
    {
        targetIntensity = Mathf.Clamp(intensity, MIN_INTENSITY, DANGEROUS_MAX_INTENSITY);
    }
}
