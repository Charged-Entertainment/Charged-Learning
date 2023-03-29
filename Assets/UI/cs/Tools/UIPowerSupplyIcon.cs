using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static Button PowerSupplyButton { get; private set; } = null;
    private class UIPowerSupplyIcon : UIBaseElement
    {
        private void Awake()
        {
            PowerSupplyButton = document.Q<Button>("power-supply-btn");
            PowerSupplyButton.RegisterCallback<ClickEvent>(ev =>
            {
                if (!PowerSupply.IsAvailable()) PowerSupply.Spawn();
            });
        }
    }
}