using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;

public class UISubmitButton : MonoBehaviour
{
    VisualElement document;
    public static Button SubmitButton;
    void Start()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        SubmitButton = document.Q<Button>("submit-btn");

        SubmitButton.RegisterCallback<ClickEvent>(HandleClick);
        GameMode.changed += HandleGameModeChange;
    }

    private void HandleClick(ClickEvent ev)
    {
        if (GameMode.Current != GameModes.Evaluate)
        {
            SubmitButton.SetEnabled(false);
            GameMode.ChangeTo(GameModes.Evaluate);
        }
        else SubmitButton.SetEnabled(true);
    }

    private void HandleGameModeChange(GameModes mode)
    {
        SubmitButton.SetEnabled(mode != GameModes.Evaluate); 
    }
}
