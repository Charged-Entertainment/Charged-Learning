using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static Button MultimeterButton { get; private set; } = null;
    private class UIMultimeterIcon : UIBaseElement
    {
        private void Awake()
        {
            MultimeterButton = document.Q<Button>("multimeter-btn");
            MultimeterButton.RegisterCallback<ClickEvent>(ev =>
            {
                if (!Multimeter.IsAvailable()) Multimeter.Spawn();
            });

            Multimeter.created += () =>
            {
                MultimeterButton.SetEnabled(false);
            };
            Multimeter.destroyed += () =>
            {
                MultimeterButton.SetEnabled(true);
            };
        }
    }
}