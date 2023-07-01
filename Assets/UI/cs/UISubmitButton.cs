using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;


public partial class UI
{
    public static Button SubmitButton { get; private set; } = null;
    private class UISubmitButton : UIBaseElement
    {
        void Start()
        {
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
}