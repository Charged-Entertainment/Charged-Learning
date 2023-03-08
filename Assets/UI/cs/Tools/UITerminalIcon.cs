using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static Button TerminalButton { get; private set; } = null;
    private class UITerminalIcon : UIBaseElement
    {
        private static VisualElement Notification;
        private void Awake()
        {
            // var terminal = rootVisualElement.Q("terminal-instance");
            TerminalButton = document.Q<Button>("terminal-btn");
            Notification = document.Q("terminal-btn-notification");
            FeebackTerminal.disabled += () =>
            {
                FeebackTerminal.LogWritten += HandleLogWritten;
                TerminalButton.SetEnabled(true);
            };

            FeebackTerminal.enabled += () =>
            {
                FeebackTerminal.LogWritten -= HandleLogWritten;
                TerminalButton.SetEnabled(false);
                HideNotification();
            };
            TerminalButton.RegisterCallback<ClickEvent>(e =>
            {
                FeebackTerminal.Enable();
            });

            Notification.visible = false;
            TerminalButton.SetEnabled(false);
        }

        private void OnEnable()
        {
            if (!FeebackTerminal.IsEnabled()) FeebackTerminal.LogWritten += HandleLogWritten;
        }

        private void OnDisable()
        {
            FeebackTerminal.LogWritten -= HandleLogWritten;
        }

        void ShowNotification(Color color)
        {
            Notification.visible = true;
            Notification.style.backgroundColor = color;
        }

        void HideNotification()
        {
            Notification.visible = false;
            Notification.style.backgroundColor = Color.black;
            Notification.style.visibility = Visibility.Hidden;
        }

        void HandleLogWritten(LogType type)
        {
            if (type is LogType.Error) ShowNotification(Color.red);
            else if (type is LogType.Warning && Notification.style.backgroundColor != Color.red) ShowNotification(Color.yellow);
            else if (type is LogType.Status && Notification.style.backgroundColor != Color.red && Notification.style.backgroundColor != Color.yellow) ShowNotification(Color.white);
        }
    }
}
