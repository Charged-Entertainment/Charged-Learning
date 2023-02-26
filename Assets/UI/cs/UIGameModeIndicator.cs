using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using GameManagement;


public partial class UI
{
    public static Image GameModeIndicator { get; private set; } = null;
    private class UIGameModeIndicator : UIBaseElement
    {
        string on = "gamemode-indicator-on";
        string off = "gamemode-indicator-off";
        void Start()
        {
            document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
            GameModeIndicator = document.Q<Image>("gamemode-indicator");
            GameMode.changed += HandleGameModeChange;
        }

        private void HandleGameModeChange(GameModes mode)
        {
            if (mode == GameModes.Edit) Off();
            else On();
        }

        void On()
        {
            GameModeIndicator.EnableInClassList(on, true);
            GameModeIndicator.EnableInClassList(off, false);
        }

        void Off()
        {
            GameModeIndicator.EnableInClassList(off, true);
            GameModeIndicator.EnableInClassList(on, false);
        }
    }
}