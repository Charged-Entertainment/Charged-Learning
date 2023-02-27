using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Symbolism;
using Symbolism.IsolateVariable;

public class Calculator : MonoBehaviour
{
    public static Dictionary<KeyCode, string> SupportedSymbols = new Dictionary<KeyCode, string>() {
        // Units
        {KeyCode.O, "Ω"},
        {KeyCode.A, "A"},
        {KeyCode.V, "V"},
        {KeyCode.W, "W"},

        // SI Prefixes
        {KeyCode.K, "k"},
        {KeyCode.M, "m"},

        // Equation
        {KeyCode.Equals, " = "},

        // Unknowns
        {KeyCode.Slash, " ? "},

        // Operstors
        {KeyCode.KeypadPlus, " + "},
        {KeyCode.KeypadMinus, " - "},
        {KeyCode.KeypadMultiply, " * "},
        {KeyCode.KeypadDivide, " / "},
        {KeyCode.P, " ^ "},
        {KeyCode.S, " √ "},

        // Space
        {KeyCode.Space, " "},

    };

    CalculatorController controller;
    private void Awake()
    {
        controller = gameObject.AddComponent<CalculatorController>();

        for (int i = 1; i <= 9; i++)
        {
            SupportedSymbols.Add(Enum.Parse<KeyCode>($"Alpha{i}"), i.ToString());
            SupportedSymbols.Add(Enum.Parse<KeyCode>($"Keypad{i}"), i.ToString());
        }

    }

    // public static float Solve(string expression) {
    //     var unknown = new Symbol("?");
    //     var eq = new Equation(0,0);
    //     if (expression.Contains('=')) {
    //         var parts = expression.Split("=");
    //         var left = parts[0].Trim();
    //         var right = parts[1].Trim();
    //     }
    //     else {
    //         Debug.Log("Not supported yet.");
    //         return 0;
    //     }
    // }
}
