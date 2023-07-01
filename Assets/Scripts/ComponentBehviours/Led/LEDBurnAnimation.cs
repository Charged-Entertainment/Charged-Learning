using UnityEngine;
using UnityEngine.Rendering.Universal;
using GameManagement;
public class LEDBurnAnimation : MonoBehaviour
{
    private static readonly int INTENSITY = 150;
    Led led;
    private void Awake()
    {
        led = gameObject.GetComponent<Led>();
        led.GetComponentInChildren<PointLight>().SetIntensityImmediate(INTENSITY);
        Invoke("GoToEditMode", 0.05f);
        // play circuit breaker loud trip SFX. 
    }

    // not done immediately because this object is added on the same frame when the gamemode changes to live/evaluate, so ChangeTo(GameModes.Live) (or Evaluate) is called after this, then this gets cancelled.
    void GoToEditMode() {
        GameMode.ChangeTo(GameModes.Edit);
        GameObject.Destroy(this);
    }
}
