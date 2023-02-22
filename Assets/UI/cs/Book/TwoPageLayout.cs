using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class TwoPageLayout : Page
{
    public Action closed;
    public Action opened;
    public VisualElement document, backgroundOverlay;
    public Image image;
    public TextElement content;
    private void Awake()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        VisualTreeAsset uxml = Resources.Load<VisualTreeAsset>("UI/TwoPageLayout");
        backgroundOverlay = uxml.Instantiate().Q(className: "BookTwoPageLayout");
        image = backgroundOverlay.Q<Image>();
        content = backgroundOverlay.Q<TextElement>();

        backgroundOverlay.RegisterCallback<ClickEvent>(Close);
        enabled = false;
    }

    private void Close(ClickEvent ev)
    {
        if (document.Contains(backgroundOverlay))
        {
            backgroundOverlay.style.opacity = 0f;
            document.Remove(backgroundOverlay);
            closed?.Invoke();
        }
    }

    private void Open(ClickEvent ev)
    {
        if (!document.Contains(backgroundOverlay))
        {
            backgroundOverlay.style.opacity = 1f;
            document.Add(backgroundOverlay);
            opened?.Invoke();
        }
    }

    private void OnEnable()
    {
        Open(null);
    }

    private void OnDisable()
    {
        Close(null);
    }
}
