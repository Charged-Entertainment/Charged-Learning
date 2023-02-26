using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static Button TerminalButton { get; private set; } = null;
    private class UITerminalIcon : UIBaseElement
    {
        private void Start()
        {
            // var terminal = rootVisualElement.Q("terminal-instance");
            TerminalButton = document.Q<Button>("terminal-btn");
            FeebackTerminal.disabled += () =>
            {
                TerminalButton.SetEnabled(true);
            };

            FeebackTerminal.enabled += () =>
            {
                TerminalButton.SetEnabled(false);
            };
            TerminalButton.RegisterCallback<ClickEvent>(e =>
            {
                FeebackTerminal.Enable();
            });
            TerminalButton.SetEnabled(false);
        }
    }
}
