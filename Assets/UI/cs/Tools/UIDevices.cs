using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDevices : MonoBehaviour
{
    VisualElement document, container;
    Button devicesBtn;

    List<Button> buttons;

    // the delay in seconds between each element showing/disappearing
    private static float delay = 0.1f;

    private void Awake()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        container = document.Q<VisualElement>("devices");
        devicesBtn = container.Q<Button>("devices-btn");
        buttons = new List<Button>();

        Button multimeterBtn = container.Q<Button>("multimeter-btn");
        buttons.Add(multimeterBtn);
        SetButtonVisable(multimeterBtn, false);

        List<Button> unknowns = container.Query<Button>(className: "unknown").ToList();
        foreach (var b in unknowns)
        {
            buttons.Add(b);
            SetButtonVisable(b, false);
        }

        InitButtonPositions();

        devicesBtn.RegisterCallback<ClickEvent>(ev =>
        {
            reverse = !reverse;
            if (!currentlyInAnimation)
            {
                currentlyInAnimation = true;
                Play();
            }
        });


        multimeterBtn.RegisterCallback<ClickEvent>(ev =>
        {
            if (!Multimeter.IsAvailable()) Multimeter.Spawn();
        });

        Multimeter.created += () => {
            // Bug!! multimeterBtn.SetEnabled(false); should not be commented out. 
            // this here is disabled becuase in the tutorial, when it fired, it disabled the button in the middle of handling the handlers subscribed to the click event, and it'd fire first, canceling all the subsequent handlers since the button is disabled. (Unity checks if the button is still enabled before calling each handler in case on disables it??)
            // multimeterBtn.SetEnabled(false);
            SetButtonVisable(multimeterBtn, true);
        };
        Multimeter.destroyed += () => {
            // multimeterBtn.SetEnabled(true);
            SetButtonVisable(multimeterBtn, true);
        };

        var terminal = document.Q("terminal-instance");
        var terminalButton = document.Q<Button>("terminal-btn");
        FeebackTerminal.disabled += () =>
        {
            terminalButton.SetEnabled(true);
        };

        FeebackTerminal.enabled += () =>
        {
            terminalButton.SetEnabled(false);
        };
        terminalButton.RegisterCallback<ClickEvent>(e =>
        {
            FeebackTerminal.Enable();
        });
        terminalButton.SetEnabled(false);
    }

    int currButtonIdx = 0;
    bool reverse = true;
    bool currentlyInAnimation = false;
    private void Play()
    {
        var b = buttons[currButtonIdx];
        SetButtonVisable(buttons[currButtonIdx], !reverse);

        if (reverse && currButtonIdx != 0)
        {
            currButtonIdx--;
            Invoke("Play", delay);
            return;
        }

        if (!reverse && currButtonIdx != buttons.Count - 1)
        {
            currButtonIdx++;
            Invoke("Play", delay);
            return;
        }

        currentlyInAnimation = false;
    }

    #region arc_related

    // the radius in pixels
    private static int r = 200;

    // the angle step in degrees 
    private static float theta = 60;

    // how many steps to skip in the beginning
    private static float offset = 0f;
    private void SetButtonVisable(Button b, bool enabled)
    {
        b.visible = enabled;
        b.style.opacity = enabled ? (b.enabledSelf? 1 : 0.5f) : 0;
    }

    private void InitButtonPositions()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Button b = buttons[i];
            b.style.top = new StyleLength(-r * Mathf.Cos((i + offset) * Mathf.Deg2Rad * theta));
            b.style.left = new StyleLength(r * Mathf.Sin((i + offset) * Mathf.Deg2Rad * theta));
        }
    }
    #endregion
}
