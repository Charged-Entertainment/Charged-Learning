using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static Button GraphingToolButton { get; private set; } = null;
    private class UIGraphingToolIcon : UIBaseElement
    {
        private void Awake()
        {
            GraphingToolButton = document.Q<Button>("graphing-tool-btn");
            GraphingToolButton.RegisterCallback<ClickEvent>(ev =>
            {
                if (!GraphingTool.IsAvailable()) GraphingTool.Spawn();
            });
        }
    }
}