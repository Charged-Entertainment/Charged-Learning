using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class UIGameModes : MonoBehaviour
{
    VisualElement document, normal, zoomIn, zoomOut, pan, wire;

    protected void Awake() {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        zoomIn = document.Q("zoom-in-btn");
        zoomOut = document.Q("zoom-out-btn");
        pan = document.Q("pan-btn");
        normal = document.Q("normal-btn");
        wire = document.Q("wiring-btn");

        Debug.Log(zoomIn);
    }

    private void OnEnable() {
        OnDisable();
        zoomIn.RegisterCallback<MouseDownEvent>(HandleZoomIn);
        zoomOut.RegisterCallback<MouseDownEvent>(HandleZoomOut);
        pan.RegisterCallback<MouseDownEvent>(HandlePan);
        normal.RegisterCallback<MouseDownEvent>(HandleNormal);
        wire.RegisterCallback<MouseDownEvent>(HandleWire);
    }

    private void OnDisable() {
        zoomIn.UnregisterCallback<MouseDownEvent>(HandleZoomIn);
        zoomOut.UnregisterCallback<MouseDownEvent>(HandleZoomOut);
        pan.UnregisterCallback<MouseDownEvent>(HandlePan);
        normal.UnregisterCallback<MouseDownEvent>(HandleNormal);
        wire.UnregisterCallback<MouseDownEvent>(HandleWire);

    }

    private void HandleZoomIn(MouseDownEvent ev) {
        Debug.Log("Zooming");
        Camera.Zoom(0.5f);
    }

    private void HandleZoomOut(MouseDownEvent ev) {
        Debug.Log("Unzooming");
        Camera.Zoom(-0.5f);
    }

    private void HandlePan(MouseDownEvent ev) {
        GameManagement.GameMode.Pan();
    }

    private void HandleNormal(MouseDownEvent ev) {
        GameManagement.GameMode.Normal();
    }

    private void HandleWire(MouseDownEvent ev) {
        GameManagement.GameMode.Wire();
    }
}
