using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Dialogs;

public class Level1 : MonoBehaviour
{
    #region UIElements' references
    UIDocument document;
    VisualElement root;
    VisualElement visualElement;

    VisualElement controlsSection;
    Button circuitBreakerBtn, hintBtn, resetBtn, pauseBtn;

    Image gameModeIndicator;

    VisualElement dialog;

    VisualElement editorControls;

    VisualElement tools;

    Button submitBtn;

    VisualElement componentsBar;
    #endregion

    #region UIElements' references for tutorial assets
    List<Image> arrows;
    #endregion

    private void Awake()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>();
        root = document.rootVisualElement;

        controlsSection = root.Q("controls-section");
        circuitBreakerBtn = controlsSection.Q<Button>("circuit-breaker");
        hintBtn = controlsSection.Q<Button>("hint-btn");
        resetBtn = controlsSection.Q<Button>("reset-btn");
        pauseBtn = controlsSection.Q<Button>("pause-btn");

        gameModeIndicator = root.Q<Image>("gamemode-indicator");

        dialog = root.Q("dialog");

        editorControls = root.Q("editor-controls");

        tools = root.Q("tools");

        submitBtn = root.Q<Button>("submit-btn");

        componentsBar = root.Q("components-bar");

        VisualTreeAsset uxml = Resources.Load<VisualTreeAsset>("Tutorials/One/content");
        StyleSheet styleSheet = Resources.Load<StyleSheet>("Tutorials/One/style");
        visualElement = uxml.Instantiate().Q("tutorial1");
        root.styleSheets.Add(styleSheet);
        root.Add(visualElement);
        // ========================================
        arrows = new List<Image>();
        for (int i = 0; i < 6; i++) arrows.Add(visualElement.Q<Image>($"arrow{i}"));
    }

    private void SetArrowEnabled(Image arrow, bool enabled)
    {
        if (enabled) arrow.RemoveFromClassList("disabled");
        else arrow.AddToClassList("disabled");
        if (enabled)
            foreach (var otherArrow in arrows)
                if (otherArrow != arrow)
                    SetArrowEnabled(otherArrow, false);
    }

    private void Start()
    {
        DisableAllSystems();
        CreateDialogSequence1();
    }

    private void DisableAllSystems()
    {
        // controlsSection.visible = false;
        circuitBreakerBtn.visible = false;
        hintBtn.visible = false;
        resetBtn.visible = false;

        gameModeIndicator.visible = false;

        editorControls.visible = false;

        tools.visible = false;

        submitBtn.visible = false;

        document.gameObject.GetComponent<UILevelComponentsCollapse>().ToggleCollapse(null);

        componentsBar.Q("components-bar-header").SetEnabled(false);
        componentsBar.Q("level-components").SetEnabled(false);

        Camera.Disable();
        Clipboard.Disable();
        FeebackTerminal.Disable();
        GameManager.Disable();
        Selection.Disable();
    }

    private void CreateDialogSequence1()
    {
        var entries = new List<DialogEntry>();
        var entry0 = new DialogEntry("init");
        entries.Add(entry0);

        var entry1 = new DialogEntry("Hi there welcome to the main editor at first let me show you around the place");
        entries.Add(entry1);

        var entry2 = new DialogEntry("First Lets discover the components we have. Click Here.");
        entries.Add(entry2);
        entry2.started += () =>
        {
            componentsBar.Q("components-bar-header").SetEnabled(true);
            SetArrowEnabled(arrows[0], true);
            Dialog.Pause();
            RegisterContinue(componentsBar.Q("components-bar-header"));
        };
        entry2.ended += () => UnregisterContinue(componentsBar.Q("components-bar-header"));

        var entry3 = new DialogEntry("Oh! You Found a Battery.");
        entries.Add(entry3);
        entry3.started += () => SetArrowEnabled(arrows[1], true);

        var entry4 = new DialogEntry("Battery Page Unlocked. A battery is a device that stores electric power in the form of chemical energy. When necessary, the energy is again released as electric power.");
        entries.Add(entry4);

        var entry5 = new DialogEntry("The collected items information can be found in the collection tab. Work hard to collect all the pages of the book as it your main weapon against the awaited danger.");
        entries.Add(entry5);
        entry5.ended += () => SetArrowEnabled(arrows[1], true);


        var entry6 = new DialogEntry("Now what are you waiting for grab the Battery abd put it in the grid to start working.");
        entries.Add(entry6);
        entry6.ended += () => SetArrowEnabled(arrows[2], true);
        var seq1 = new DialogSequence(entries);

        Dialog.PlaySequence(seq1);
    }

    public void RegisterContinue(VisualElement v) {
        v.RegisterCallback<ClickEvent>(Dialog.Continue);
    }

    public void UnregisterContinue(VisualElement v) {
        v.UnregisterCallback<ClickEvent>(Dialog.Continue);
    }

}
