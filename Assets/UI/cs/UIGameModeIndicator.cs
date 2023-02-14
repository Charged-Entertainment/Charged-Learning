using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;

public class UIGameModeIndicator : MonoBehaviour
{
    VisualElement document;
    Image image;

    string on = "gamemode-indicator-on";
    string off = "gamemode-indicator-off";
    void Start()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        image = document.Q<Image>("gamemode-indicator");
        GameMode.changed += HandleGameModeChange;
    }

    private void HandleGameModeChange(GameModes mode) {
        if (mode == GameModes.Edit) Off();
        else On();
    }

    void On() {
        image.EnableInClassList(on, true);
        image.EnableInClassList(off, false);
    }

    void Off() {
        image.EnableInClassList(off, true);
        image.EnableInClassList(on, false);
    }
}
