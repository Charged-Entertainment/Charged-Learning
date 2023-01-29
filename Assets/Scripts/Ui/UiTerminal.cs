using System.Collections;
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
    [SerializeField]private List<string> logs;
    [SerializeField]private List<string> testGoals;
    private VisualElement goalsVisualElement;
    private VisualElement logsVisualElement;
    private VisualElement bodyVisualElement;
    private VisualElement terminalWindow;

    private WindowState windowState;
    [SerializeField] private bool drawLogs;

    void Start()
    {
        logs = new List<string>{"log1", "log2"};
        windowState = WindowState.Normal;
        drawLogs = false;
    }

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        goalsVisualElement = rootVisualElement.Q("terminal-instance").Q("goals");
        logsVisualElement = rootVisualElement.Q("terminal-instance").Q("logs");
        bodyVisualElement = rootVisualElement.Q("terminal-instance").Q("body");
        terminalWindow = rootVisualElement.Q("terminal-instance").Q("window");

        rootVisualElement.Q<Button>("maximize-btn").RegisterCallback<ClickEvent>(Maximize);
        rootVisualElement.Q<Button>("minimize-btn").RegisterCallback<ClickEvent>(Minimize);
        testGoals = new List<string>{"measure whatever in whatever and so whatever","measure whatever in whatever","measure whatever in whatever"};

        DrawGoals();
    }

    private void Update() {
        if(drawLogs){
            DrawLogs();
            drawLogs = false;
        }
    }

    public void Write(string log)
    {
        logs.Add(log);
        DrawLogs();

    }

    private void Maximize(ClickEvent ev)
    {
        if (windowState != WindowState.Maximized)
        {
            Debug.Log("Maximize clicked!");
            terminalWindow.style.height = Length.Percent(40);

            windowState = WindowState.Maximized;
        }
        else
        {
            Debug.Log("Normal clicked!");
            terminalWindow.style.height = Length.Percent(30);
            // terminalWindow.style.width = Length.Percent(30);
            windowState = WindowState.Normal;
        }
        bodyVisualElement.style.display = DisplayStyle.Flex;
        DrawLogs();

    }

    private void Minimize(ClickEvent ev)
    {
        Debug.Log("minimize clicked!");
        
        bodyVisualElement.style.display = DisplayStyle.None;
        windowState = WindowState.Minimized;

    }

    private void DrawLogs()
    {
        if(windowState == WindowState.Normal){
            logsVisualElement.Clear();
            Debug.Log(logs.Count);
            logsVisualElement.Add(new Label($"<indent=30em>-{logs.LastOrDefault()}</indent>"));

        }
        if(windowState == WindowState.Maximized){
            logsVisualElement.Clear();
            foreach(var log in logs){
                logsVisualElement.Add(new Label($"<indent=30em>-{log}</indent>"));
            }
        }
        if(windowState == WindowState.Normal){}
    }

    private void DrawGoals(){
        if(windowState != WindowState.Minimized){
            goalsVisualElement.Clear();
            foreach(var goal in testGoals){
                goalsVisualElement.Add(new Label($"<indent=30em><b><s>-{goal}</s></b></indent>"));
            }
        }
    }

}
