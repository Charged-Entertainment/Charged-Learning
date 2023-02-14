using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;

public class UICircuitBreaker : MonoBehaviour
{
    VisualElement document;
    Button breaker;

    string down = "circuit-breaker-down";
    void Start()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        breaker = document.Q<Button>("circuit-breaker");

        breaker.RegisterCallback<ClickEvent>(HandleClick);
        GameMode.changed += HandleGameModeChange;
    }

    private void HandleClick(ClickEvent ev) {
        if (GameMode.Current == GameModes.Edit) GameMode.ChangeTo(GameModes.Live);
        else if (GameMode.Current == GameModes.Live) GameMode.ChangeTo(GameModes.Edit);
        else Debug.Log("Error");
    }

    private void HandleGameModeChange(GameModes mode) {
        breaker.SetEnabled(mode != GameModes.Evaluate);
        if (mode == GameModes.Live) Down();
        if (mode == GameModes.Edit) Up();
    }

    void Up() {
        breaker.EnableInClassList(down, true);
    }

    void Down() {
        breaker.EnableInClassList(down, false);
    }
}
