using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static VisualElement HintButton { get; private set; } = null;
    public static VisualElement ResetButton { get; private set; } = null;
    public static VisualElement PauseButton { get; private set; } = null;
    private class UILevelControls : UIBaseElement
    {
        protected void Awake()
        {
            HintButton = document.Q("hint-btn");
            ResetButton = document.Q("reset-btn");
            PauseButton = document.Q("pause-btn");
        }
        // private void OnEnable()
        // {
        //     OnDisable();
        // }

        // private void OnDisable()
        // {

        // }
    }
}