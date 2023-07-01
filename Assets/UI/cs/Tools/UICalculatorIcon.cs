using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static Button CalculatorButton { get; private set; } = null;
    private class UICalculatorIcon : UIBaseElement
    {
        private void Awake()
        {
            // var terminal = rootVisualElement.Q("terminal-instance");
            CalculatorButton = document.Q<Button>("calculator-btn");
            Calculator.destroyed += () =>
            {
                CalculatorButton.SetEnabled(true);
            };

            Calculator.created += () =>
            {
                CalculatorButton.SetEnabled(false);
            };
            CalculatorButton.RegisterCallback<ClickEvent>(e =>
            {
                Calculator.Spawn();
            });
        }
    }
}