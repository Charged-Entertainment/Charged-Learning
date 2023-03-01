using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using AngouriMath;

public class Calculator : EditorBehaviour
{
    public static HashSet<char> SupportedSymbols = new HashSet<char>() {
        //Units
        {'o'}, {'a'}, {'v'}, {'w'},

        // SI Prefixes
        {'k'},{'m'},

        // Equation
        {'='},

        // Unknowns
        {'?'},

        // Operstors
        {'+'}, {'-'}, {'*'}, {'/'}, {'^'}, {'s'},{'√'},

        // Space
        {' '},
        // Paranthesis
        {'('}, {')'},
        // Dot
        {'.'}
    };

    public static Dictionary<char, char> symbolTranslation = new Dictionary<char, char>(){
        // Units
        {'o', 'Ω'},
        {'a', 'A'},
        {'v', 'V'},
        {'w', 'W'},

        // Operstors
        {'s', '√'},
    };

    public static Dictionary<string, string> unitTranslation = new Dictionary<string, string>(){
        {@"((A \* Ω)|(Ω \* A))", "V"},
        {@"((A \* V)|(V \* A))", "W"},
        {@"(W / A)", "V"},
    };



    private static CalculatorController controller;
    private void Start()
    {
        controller = gameObject.AddComponent<CalculatorController>();
        created?.Invoke();
    }


    private static string PrepareExpression(string expression)
    {
        expression = expression.Replace("√", "sqrt");
        expression = expression.Replace("m", "*(10^-3)");
        expression = expression.Replace("k", "*(10^3)");
        return expression;
    }
    public static void Solve(string expression)
    {
        string result = "";
        expression = PrepareExpression(expression);
        if (expression.Contains("?"))
        {
            //TODO: check if it follows the pattern for an equation
            expression = expression.Replace('?', 'x');
            Entity equation = expression;
            result = equation.Solve("x").Stringize();
        }
        else
        {
            Entity equation = expression;
            result = equation.Simplify().Stringize();
        }
        foreach(var pair in unitTranslation){
            result = Regex.Replace(result, pair.Key, pair.Value);
        }
        controller.Display(result);
    }

    public static bool SymbolSupported(char c)
    {
        return SupportedSymbols.Contains(Char.ToLower(c));
    }

    public static char TranslateSymbol(char c)
    {
        if (!symbolTranslation.ContainsKey(c))
            return c;
        return symbolTranslation[c];
    }

    public new static Action created, destroyed;
    public static Action<MultimeterMode> modeChanged;
    public LiveComponent ConnectedComponent { get; set; }

    private new void OnDestroy()
    {
        base.OnDestroy();
        destroyed?.Invoke();
    }

    public static void Spawn()
    {
        var inScene = GameObject.Find("Calculator");
        if (inScene != null) Debug.Log("Calculator already in scene, cannot spawn.");
        else GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/Devices/Calculator"));
    }
    public static bool IsAvailable()
    {
        var inScene = GameObject.Find("Calculator");
        return inScene != null;
    }

    public static new void Destroy()
    {
        var inScene = GameObject.Find("Calculator");
        if (inScene == null) Debug.Log("Calculator not in scene, cannot destroy.");
        else GameObject.Destroy(inScene);
    }
}
