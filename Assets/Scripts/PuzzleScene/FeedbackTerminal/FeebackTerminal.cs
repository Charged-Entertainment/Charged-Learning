using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class FeebackTerminal : Singleton<FeebackTerminal>
{

    enum WindowState { Normal, Maximized, Minimized }

    public new static Action enabled;
    public static Action disabled;


    #region privates
    [SerializeField] private List<Log> logs;
    private VisualElement rootVisualElement, goalsVisualElement, logsVisualElement, bodyVisualElement, terminalWindow, terminalInstance;
    private bool mouseDown = false;
    private Vector2 dragStartPos;
    private WindowState windowState;
    #endregion

    #region publics
    public static Action<LogType> LogWritten;
    #endregion

    void OnEnable()
    {
        enabled?.Invoke();
        logs = new List<Log> {
            new Log(RichText.Color("log1", PaletteColor.Green), LogType.Status),
            new Log("log2", LogType.Status),
            new Log(RichText.Color("log3", PaletteColor.Red), LogType.Error),
            new Log(RichText.Italic(RichText.Bold("log4")), LogType.Error)
            };

        rootVisualElement = UI.GetRootVisualElement();
        terminalInstance = rootVisualElement.Q("terminal-instance");

        goalsVisualElement = terminalInstance.Q("goals");
        logsVisualElement = terminalInstance.Q("logs");
        bodyVisualElement = terminalInstance.Q("body");
        terminalWindow = terminalInstance.Q("window");

        rootVisualElement.Q<Button>("maximize-btn").RegisterCallback<ClickEvent>(e => MaximizeToggle());
        rootVisualElement.Q<Button>("minimize-btn").RegisterCallback<ClickEvent>(Minimize);

        terminalWindow.RegisterCallback<MouseDownEvent>(ev =>
        {
            if (ev.button != 0) return;
            mouseDown = true;
            dragStartPos = Utils.ScreenToPanelPosition(rootVisualElement.panel,
                                                Input.mousePosition);
        });
        terminalWindow.RegisterCallback<MouseUpEvent>(ev => mouseDown = false);

        DrawGoals();

        Puzzle.goalsChanged += DrawGoals;
        Puzzle.goalAchieved += (goal) => DrawGoals();

        windowState = WindowState.Maximized;
        MaximizeToggle();

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (mouseDown)
        {
            HandleMouseDrag(Utils.ScreenToPanelPosition(rootVisualElement.panel,
                                                    Input.mousePosition));
        }

    }

    private void FixedUpdate()
    {

    }

    /// <summary>Writes logs to the terminal window</summary>
    public static void Write(Log log)
    {
        Instance.logs.Add(log);
        Instance.DrawLogs();
        LogWritten?.Invoke(log.Type);

    }

    public new static void Enable() {
        enabled?.Invoke();
        Instance.terminalInstance.visible = true;
    }

    public new static void Disable() {
        disabled?.Invoke();
        Instance.terminalInstance.visible = false;
    }

    public static bool IsEnabled() {
        return Instance.terminalInstance.visible;
    }

    #region maximize minimize
    private void MaximizeToggle()
    {
        terminalInstance.style.height = Length.Percent(50);
        terminalWindow.style.width = Length.Percent(70);

        if (windowState != WindowState.Maximized)
        {
            terminalWindow.style.height = Length.Percent(70);

            windowState = WindowState.Maximized;
        }
        else
        {
            terminalWindow.style.height = Length.Percent(50);
            windowState = WindowState.Normal;
        }
        bodyVisualElement.style.display = DisplayStyle.Flex;
        DrawLogs();

    }

    private void Minimize(ClickEvent ev)
    {
        // bodyVisualElement.style.display = DisplayStyle.None;
        // windowState = WindowState.Minimized;
        // terminalInstance.style.height = Length.Percent(20);
        Disable();
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

    private RichText DrawGoal(Goal goal){
        Debug.Log($"DrawGoal: Achieved? {goal.Achieved}");
        if(goal.Achieved)
            return RichText.StrikeThrough(goal.Message);
        
        return goal.Message;
    }

    private void DrawGoals()
    {
        var levelGoals = Puzzle.Goals;

        if (windowState != WindowState.Minimized)
        {
            goalsVisualElement.Clear();
            foreach (var goal in levelGoals)
            {

                goalsVisualElement.Add(new Label($"<indent=30em>-{DrawGoal(goal)}</indent>"));
            }
        }
    }

    private void HandleMouseDrag(Vector2 mousePosition)
    {
        if (mouseDown)
        {
            terminalWindow.style.top = terminalWindow.resolvedStyle.top + (mousePosition.y - dragStartPos.y);
            terminalWindow.style.left = terminalWindow.resolvedStyle.left + (mousePosition.x - dragStartPos.x);
            dragStartPos = mousePosition;
        }
    }
}
