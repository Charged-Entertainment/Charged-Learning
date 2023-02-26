using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;

public partial class UI
{
    public static Button CircuitBreaker { get; private set; } = null;
    private class UICircuitBreaker : UIBaseElement
    {
        string down = "circuit-breaker-down";
        void Start()
        {
            CircuitBreaker = document.Q<Button>("circuit-breaker");

            CircuitBreaker.RegisterCallback<ClickEvent>(HandleClick);
            GameMode.changed += HandleGameModeChange;
        }

        private void HandleClick(ClickEvent ev)
        {
            if (GameMode.Current == GameModes.Edit) GameMode.ChangeTo(GameModes.Live);
            else if (GameMode.Current == GameModes.Live) GameMode.ChangeTo(GameModes.Edit);
            else Debug.Log("Error");
        }

        private void HandleGameModeChange(GameModes mode)
        {
            CircuitBreaker.SetEnabled(mode != GameModes.Evaluate);
            if (mode == GameModes.Live) Down();
            if (mode == GameModes.Edit) Up();
        }

        void Up()
        {
            CircuitBreaker.EnableInClassList(down, true);
        }

        void Down()
        {
            CircuitBreaker.EnableInClassList(down, false);
        }
    }
}