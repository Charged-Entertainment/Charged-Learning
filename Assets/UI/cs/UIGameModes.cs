using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class UIGameModes : MonoBehaviour
{
    VisualElement document, normal, zoomIn, zoomOut, pan;

    protected void Awake() {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        zoomIn = document.Q("zoom-in-btn");
        zoomOut = document.Q("zoom-out-btn");
        pan = document.Q("pan-btn");
        normal = document.Q("normal-btn");
    }

    private void OnEnable() {
        OnDisable();
        zoomIn.RegisterCallback<ClickEvent>(HandleZoomIn);
        zoomOut.RegisterCallback<ClickEvent>(HandleZoomOut);
        pan.RegisterCallback<ClickEvent>(HandlePan);
        normal.RegisterCallback<ClickEvent>(HandleNormal);
    }

    private void OnDisable() {
        zoomIn.UnregisterCallback<ClickEvent>(HandleZoomIn);
        zoomOut.UnregisterCallback<ClickEvent>(HandleZoomOut);
        pan.UnregisterCallback<ClickEvent>(HandlePan);
        normal.UnregisterCallback<ClickEvent>(HandleNormal);

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
