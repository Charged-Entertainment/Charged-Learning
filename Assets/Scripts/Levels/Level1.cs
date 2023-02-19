using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Dialogs;
using Components;
using GameManagement;

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
    LevelComponent battery, resistor;
    UILevelComponent UIBattery, UIResistor;
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
        InitializeAssets();
        DisableAllSystems();
        PlayDialogSequence1();
    }

    private void InitializeAssets()
    {
        entries = new List<DialogEntry>();
        arrows = new List<Image>();
        for (int i = 0; i < 6; i++) arrows.Add(visualElement.Q<Image>($"arrow{i}"));
        CreateBattery();
    }

    private void CreateBattery()
    {
        UILevelComponent.created += InitBatteryUI;
        battery = Puzzle.CreateLevelComponent("battery", ComponentType.Battery, 1);
        Puzzle.AddProperty(battery, PropertyType.Voltage, 12, 1, "v", true);
    }
    private void InitBatteryUI(UILevelComponent c)
    {
        if (c.levelComponent.Name == "battery")
        {
            UIBattery = c;
            UILevelComponent.created -= InitBatteryUI;
        }
    }

    private void CreateResistor()
    {
        UILevelComponent.created += InitResisorUI;
        resistor = Puzzle.CreateLevelComponent("resistor", ComponentType.Battery, 5);
        Puzzle.AddProperty(resistor, PropertyType.Resistance, 5, 1, "ohm", true);
    }
    private void InitResisorUI(UILevelComponent c)
    {
        if (c.levelComponent.Name == "resistor")
        {
            UIResistor = c;
            UILevelComponent.created -= InitResisorUI;
        }
    }

    private void SetEnabledVisable(VisualElement v, bool enabled, bool? visible = null)
    {
        if (visible == null) visible = enabled;
        v.SetEnabled(enabled);
        v.visible = visible.Value;
    }

    private void DisableAllSystems()
    {
        // controlsSection.visible = false;
        circuitBreakerBtn.visible = false;
        hintBtn.visible = false;
        resetBtn.visible = false;

        gameModeIndicator.visible = false;

        SetEnabledVisable(editorControls.Q("book-btn"), false);
        SetEnabledVisable(editorControls.Q("normal-btn"), false);
        SetEnabledVisable(editorControls.Q("zoom-in-btn"), false);
        SetEnabledVisable(editorControls.Q("zoom-out-btn"), false);
        SetEnabledVisable(editorControls.Q("wiring-btn"), false);
        SetEnabledVisable(editorControls.Q("pan-btn"), false);

        SetEnabledVisable(tools.Q("devices"), false);
        SetEnabledVisable(tools.Q("terminal-btn"), false);


        submitBtn.visible = false;

        document.gameObject.GetComponent<UILevelComponentsCollapse>().ToggleCollapse(null);

        componentsBar.Q("components-bar-header").SetEnabled(false);
        componentsBar.Q("level-components").SetEnabled(false);

        Camera.Disable();
        Clipboard.Disable();
        FeebackTerminal.Disable();
        GameManager.Disable();
        Selection.Disable();
        WireManager.Disable();
    }

    private List<DialogEntry> entries;
    private DialogEntry lastEntry;

    private void AddEntry(RichText content)
    {
        var t = new DialogEntry(content);
        entries.Add(t);
        lastEntry = t;
    }

    private void PlayDialogSequence1()
    {
        // 0
        AddEntry("init");

        // 1
        AddEntry("Hi there welcome to the main editor at first let me show you around the place");

        // 2
        AddEntry("First Lets discover the components we have. Click Here.");
        lastEntry.started += () =>
        {
            UIBattery.visualElement.SetEnabled(false);
            componentsBar.Q("components-bar-header").SetEnabled(true);
            SetArrowEnabled(arrows[0], true);
            Dialog.Pause();
            ContinueOnClick(componentsBar.Q("components-bar-header"));
        };
        lastEntry.ended += () =>
        {
            SetArrowEnabled(arrows[0], false);
            UnregisterContinue(componentsBar.Q("components-bar-header"));
        };

        // 3
        AddEntry("Oh! You Found a Battery.");

        // 4
        AddEntry("Battery Page Unlocked. A battery is a device that stores electric power in the form of chemical energy. When necessary, the energy is again released as electric power.");
        lastEntry.started += () =>
        {
            SetArrowEnabled(arrows[2], true);
            SetEnabledVisable(editorControls.Q("book-btn"), true);
        };
        lastEntry.ended += Book.ShowEmpty;

        // 5
        AddEntry("The collected items information can be found in the collection tab. Work hard to collect all the pages of the book as it your main weapon against the awaited danger.");

        // 6
        AddEntry("Now what are you waiting for grab the Battery abd put it in the grid to start working.");
        lastEntry.started += () =>
        {
            UIBattery.visualElement.SetEnabled(true);
            SetArrowEnabled(arrows[1], true);
            Dialog.Pause();
            ContinueOnClick(UIBattery.visualElement);
        };
        lastEntry.ended += () => UnregisterContinue(UIBattery.visualElement);

        // 7
        AddEntry("Did you know!! YOU can move through as you like through the grid either using the tools up there or the shortcut for each tool");
        lastEntry.started += () =>
        {
            SetArrowEnabled(arrows[3], true);

            SetEnabledVisable(editorControls.Q("normal-btn"), true);
            SetEnabledVisable(editorControls.Q("zoom-in-btn"), true);
            SetEnabledVisable(editorControls.Q("zoom-out-btn"), true);
            SetEnabledVisable(editorControls.Q("pan-btn"), true);

            GameManager.Enable();
            Selection.Enable();
            Camera.Enable();
        };
        lastEntry.ended += () => SetArrowEnabled(arrows[3], false);

        // 8
        AddEntry("As we mentioned, the Battery is a DC power source. But we don't know how much energy it gives us. This energy is called Voltage which has it’s own measuring unit Volts");

        // 9
        AddEntry("To measure The voltage of the battery we can a use a Multi-meter (Multi-meter Unlocked). Open the devices menu!");
        lastEntry.started += () =>
        {
            Dialog.Pause();
            SetArrowEnabled(arrows[4], true);
            SetEnabledVisable(tools.Q("devices"), true);
            ContinueOnClick(tools.Q("devices-btn"));
        };
        lastEntry.ended += () => UnregisterContinue(tools.Q("devices-btn"));

        // 10 
        AddEntry("Now spawn the multi-meter in the editor by clicking on its icon.");
        lastEntry.started += () =>
        {
            Dialog.Pause();
            SetArrowEnabled(arrows[5], true);
            ContinueOnClick(tools.Q("multimeter-btn"));
        };
        lastEntry.ended += () => UnregisterContinue(tools.Q("multimeter-btn"));

        // 11 

        System.Action<LevelComponent> HandlePropertyRevealed = (component =>
            {
                    Dialog.Continue();
            });

        AddEntry(RichText.Bold(RichText.Color("Okay the multi-meter is now in the editor. Connect its probe to the battery and set its mode to voltage then measure the battery", PaletteColor.Red)));
        lastEntry.started += () =>
        {
            Dialog.Pause();
            SetArrowEnabled(arrows[5], false);
            SetEnabledVisable(editorControls.Q("wiring-btn"), true);
            WireManager.Enable();
            SetEnabledVisable(submitBtn, true);
            LevelComponent.propertyRevealed += HandlePropertyRevealed;
        };

        lastEntry.ended += () => LevelComponent.propertyRevealed -= HandlePropertyRevealed;

        AddEntry("hello");


        // WiP...

        var seq1 = new DialogSequence(entries);
        Dialog.PlaySequence(seq1);
    }

    public void ContinueOnClick(VisualElement v)
    {
        v.RegisterCallback<ClickEvent>(Dialog.Continue);
    }

    public void UnregisterContinue(VisualElement v)
    {
        v.UnregisterCallback<ClickEvent>(Dialog.Continue);
    }

}
