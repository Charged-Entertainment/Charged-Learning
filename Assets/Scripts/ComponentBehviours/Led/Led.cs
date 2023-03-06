using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Led : MonoBehaviour
{
    public readonly static float MIN_INTENSITY = 0;
    public readonly static float MAX_INTENSITY = 22.5f;

    [SerializeField] private new Light2D light;
    [SerializeField] public float lerpSpeed = 3;
    [SerializeField] private float targetIntensity = 0;
    
    private void Awake() {
        light = gameObject.GetComponent<Light2D>();
        light.intensity = 0;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PuzzleScene") {
            gameObject.AddComponent<LedCurrentBehvior>();
        }
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
        targetIntensity = Mathf.Clamp(intensity, MIN_INTENSITY, MAX_INTENSITY);
        if (intensity != targetIntensity) {
            Debug.Log($"Warning: LED intensity value clamped! ({intensity})");
        }
    }
}
