using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class UIGameModes : MonoBehaviour
{
    VisualElement document, zoomIn, zoomOut;

    protected void Awake() {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        zoomIn = document.Q("zoom-in-btn");
        zoomOut = document.Q("zoom-out-btn");

        Debug.Log(zoomIn);
    }

    private void OnEnable() {
        OnDisable();
        zoomIn.RegisterCallback<MouseDownEvent>(HandleZoomIn);
        zoomOut.RegisterCallback<MouseDownEvent>(HandleZoomOut);
    }

    private void OnDisable() {
        zoomIn.UnregisterCallback<MouseDownEvent>(HandleZoomIn);
        zoomOut.UnregisterCallback<MouseDownEvent>(HandleZoomOut);
    }

    private void HandleZoomIn(MouseDownEvent ev) {
        Debug.Log("Zooming");
        Camera.Zoom(0.5f);
    }

    private void HandleZoomOut(MouseDownEvent ev) {
        Debug.Log("Unzooming");
        Camera.Zoom(-0.5f);
    }
}
