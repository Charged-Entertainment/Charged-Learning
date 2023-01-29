using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
enum WindowState
{
    Normal,
    Maximized,
    Minimized
}

public class UiTerminal : MonoBehaviour
{
    #region privates
    [SerializeField] private List<Log> logs;
    [SerializeField] private List<string> testGoals;
    private VisualElement goalsVisualElement;
    private VisualElement logsVisualElement;
    private VisualElement bodyVisualElement;
    private VisualElement terminalWindow;
    private bool mouseDown = false;
    private Vector2 dragStartPos;
    private WindowState windowState;
    #endregion

    #region publics
    public Action<LogType> LogWritten;
    #endregion

    void OnEnable()
    {
        testGoals = new List<string> { "measure whatever in whatever and so whatever", "measure whatever in whatever", "measure whatever in whatever" };
        logs = new List<Log> {
            new Log(RichText.Color("log1", PaletteColor.Green), LogType.Status),
            new Log("log2", LogType.Status),
            new Log(RichText.Color("log3", PaletteColor.Red), LogType.Error),
            new Log(RichText.Italic(RichText.Bold("log4")), LogType.Error)
            };

        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        var terminalInstance = rootVisualElement.Q("terminal-instance");

        goalsVisualElement = terminalInstance.Q("goals");
        logsVisualElement = terminalInstance.Q("logs");
        bodyVisualElement = terminalInstance.Q("body");
        terminalWindow = terminalInstance.Q("window");

        rootVisualElement.Q<Button>("maximize-btn").RegisterCallback<ClickEvent>(e => MaximizeToggle());
        rootVisualElement.Q<Button>("minimize-btn").RegisterCallback<ClickEvent>(Minimize);

        terminalWindow.RegisterCallback<MouseDownEvent>(ev => { mouseDown = true; dragStartPos = ev.mousePosition; });
        terminalWindow.RegisterCallback<MouseUpEvent>(ev => mouseDown = false);
        terminalWindow.RegisterCallback<MouseMoveEvent>(HandleMouseDrag);
        terminalWindow.RegisterCallback<MouseLeaveEvent>(ev => mouseDown = false);


        DrawGoals();

        windowState = WindowState.Maximized;
        MaximizeToggle();

    }

    /// <summary>Writes logs to the terminal window</summary>
    public void Write(Log log)
    {
        logs.Add(log);
        DrawLogs();
        LogWritten.Invoke(log.Type);

    }
    #region maximize minimize
    private void MaximizeToggle()
    {
        if (windowState != WindowState.Maximized)
        {
            terminalWindow.style.height = Length.Percent(40);

            windowState = WindowState.Maximized;
        }
        else
        {
            terminalWindow.style.height = Length.Percent(30);
            // terminalWindow.style.width = Length.Percent(30);
            windowState = WindowState.Normal;
        }
        bodyVisualElement.style.display = DisplayStyle.Flex;
        DrawLogs();

    }

    private void Minimize(ClickEvent ev)
    {
        bodyVisualElement.style.display = DisplayStyle.None;
        windowState = WindowState.Minimized;
    }
    #endregion

    /// <summary>
    ///Draws the Logs list to the terminal window
    ///depending on its current state
    ///</summary>
    private void DrawLogs()
    {
        if (windowState == WindowState.Normal)
        {
            logsVisualElement.Clear();
            logsVisualElement.Add(new Label($"<indent=30em>-{logs.LastOrDefault().Message}</indent>"));

        }
        if (windowState == WindowState.Maximized)
        {
            logsVisualElement.Clear();
            foreach (var log in logs)
            {
                logsVisualElement.Add(new Label($"<indent=30em>-{log.Message}</indent>"));
            }
        }
    }

    private void DrawGoals()
    {
        if (windowState != WindowState.Minimized)
        {
            goalsVisualElement.Clear();
            foreach (var goal in testGoals)
            {
                goalsVisualElement.Add(new Label($"<indent=30em><b><s>-{goal}</s></b></indent>"));
            }
        }
    }

    private void HandleMouseDrag(MouseMoveEvent ev)
    {
        if (mouseDown)
        {
            terminalWindow.style.top = terminalWindow.resolvedStyle.top + (ev.mousePosition.y - dragStartPos.y);
            terminalWindow.style.left = terminalWindow.resolvedStyle.left + (ev.mousePosition.x - dragStartPos.x);
            dragStartPos = ev.mousePosition;
        }
    }

}

