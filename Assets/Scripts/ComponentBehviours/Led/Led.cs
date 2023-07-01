using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Led : MonoBehaviour
{
    public readonly static float MAX_INTENSITY = 22.5f;
    [SerializeField] private new PointLight light;
    private void Awake() {
        light = gameObject.GetComponentInChildren<PointLight>();
        light.SetIntensityImmediate(0, false);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PuzzleScene") {
            gameObject.AddComponent<LedCurrentBehvior>();
        }
    }

    public void SetIntensity(float intensity)
    {
        var targetIntensity = Mathf.Clamp(intensity, 0, MAX_INTENSITY);
        light.SetIntensity(targetIntensity);
        if (intensity != targetIntensity) {
            Debug.Log($"Warning: LED intensity value clamped! ({intensity})");
        }
    }
}
