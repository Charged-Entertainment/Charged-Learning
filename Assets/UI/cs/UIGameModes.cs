using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class UIGameModes : MonoBehaviour
{
    VisualElement document;
    public static VisualElement NormalButton, ZoomInButton, ZoomOutButton, PanButton;

    protected void Awake() {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        ZoomInButton = document.Q("zoom-in-btn");
        ZoomOutButton = document.Q("zoom-out-btn");
        PanButton = document.Q("pan-btn");
        NormalButton = document.Q("normal-btn");
    }

    private void OnEnable() {
        OnDisable();
        ZoomInButton.RegisterCallback<ClickEvent>(HandleZoomIn);
        ZoomOutButton.RegisterCallback<ClickEvent>(HandleZoomOut);
        PanButton.RegisterCallback<ClickEvent>(HandlePan);
        NormalButton.RegisterCallback<ClickEvent>(HandleNormal);
    }

    private void OnDisable() {
        ZoomInButton.UnregisterCallback<ClickEvent>(HandleZoomIn);
        ZoomOutButton.UnregisterCallback<ClickEvent>(HandleZoomOut);
        PanButton.UnregisterCallback<ClickEvent>(HandlePan);
        NormalButton.UnregisterCallback<ClickEvent>(HandleNormal);

    }

    private void HandleZoomIn(ClickEvent ev) {
        Debug.Log("Zooming");
        Camera.Zoom(0.5f);
    }

    private void HandleZoomOut(ClickEvent ev) {
        Debug.Log("Unzooming");
        Camera.Zoom(-0.5f);
    }

    private void HandlePan(ClickEvent ev) {
        GameManagement.GameMode.Pan();
    }

    private void HandleNormal(ClickEvent ev) {
        GameManagement.GameMode.Normal();
    }
}
