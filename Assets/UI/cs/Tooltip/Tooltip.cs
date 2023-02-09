using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using UnityEngine.UIElements;

abstract public class Tooltip
{
    public VisualElement root { get; protected set; }
    public static VisualElement document { get; protected set; }

    protected Tooltip()
    {
        VisualTreeAsset uxml = Resources.Load<VisualTreeAsset>("Tooltip");
        root = uxml.Instantiate().Q("tooltip");
    }

    private class TooltipLoader : Singleton<TooltipLoader>
    {
        private new void Awake()
        {
            base.Awake();
            document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

            UILevelComponent.created += HandleUILevelComponentCreated;
            ComponentManager.created += HandleLiveComponentCreated;

            // TODO: load DefaultTooltipVE's for static UI elements like menus, editor controls buttons, etc...
        }

        private static void HandleUILevelComponentCreated(UILevelComponent c)
        {
            var t = new DefaultTooltipVE(c.visualElement, c.levelComponent.Name, "ctrl +", "Testo besto");
            // TODO: set the position of t.root to somewhere near c.visualElement
        }

        private static void HandleLiveComponentCreated(ComponentBehavior c)
        {
            var t = new DefaultTooltipCB(c, c.levelComponent.Name, "ctrl +", "Testo besto");
            // TODO: set the position of t.root to somewhere near c.gameObject.transform.position
        }
    }
}

public abstract class DefaultTooltip : Tooltip
{
    public string Title;
    public string Subtitle;
    public string Description;

    public DefaultTooltip(string title, string subtitle = null, string description = null) : base()
    {
        Title = title;
        Subtitle = subtitle;
        Description = description;

        var t = new Label(title);
        t.AddToClassList("title");
        root.Add(t);

        if (subtitle != null)
        {
            var label = new Label(subtitle);
            t.Add(label);
            label.AddToClassList("subtitle");
        }

        if (description != null)
        {
            t = new Label(description);
            t.AddToClassList("description");
            root.Add(t);
        }
    }
}

public class DefaultTooltipVE : DefaultTooltip
{
    VisualElement VisualElement;
    public DefaultTooltipVE(VisualElement visualElement, string title, string subtitle = null, string description = null) : base(title, subtitle, description)
    {
        VisualElement = visualElement;
        OnEnable();
    }

    private void OnEnable()
    {
        VisualElement.RegisterCallback<MouseEnterEvent>(e => document.Add(root));
        VisualElement.RegisterCallback<MouseLeaveEvent>(e => document.Remove(root));
    }
}

public class DefaultTooltipCB : DefaultTooltip
{
    ComponentBehavior component;
    public DefaultTooltipCB(ComponentBehavior visualElement, string title, string subtitle = null, string description = null) : base(title, subtitle, description)
    {
        component = visualElement;
        ComponentManager.mouseEntered += c => { if (c == component) document.Add(root); };
        ComponentManager.mouseExited += c => { if (c == component) document.Remove(root); };
        // TODO: this object will remain in memory, find a way for it to be garbage collected.
        ComponentManager.destroyed += c => { if (c == component) document.Remove(root); };
    }
}

// TODO: add LevelComponentToolip (includes key-value pairs of values)