using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;

public class UISubmitButton : MonoBehaviour
{
    VisualElement document;
    Button submit;
    void Start()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        submit = document.Q<Button>("submit-btn");

        submit.RegisterCallback<ClickEvent>(HandleClick);
        GameMode.changed += HandleGameModeChange;
    }

    private void HandleClick(ClickEvent ev)
    {
        if (GameMode.Current != GameModes.Evaluate)
        {
            submit.SetEnabled(false);
            GameMode.ChangeTo(GameModes.Evaluate);
        }
        else submit.SetEnabled(true);
    }

    private void HandleGameModeChange(GameModes mode)
    {
        submit.SetEnabled(mode != GameModes.Evaluate); 
    }
}
