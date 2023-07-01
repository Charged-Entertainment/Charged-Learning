using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using AngouriMath;
using AngouriMath.Extensions;

public class Calculator : EditorBehaviour
{
    public static readonly char cLOWER_OMEGA = 'ω';
    public static readonly char cUPPER_OMEGA = 'Ω';
    public static readonly string sUPPER_OMEGA = "Ω";
    public static readonly string sLOWER_OMEGA = "ω";
    public static HashSet<char> SupportedSymbols = new HashSet<char>() {
        //Units
        {cLOWER_OMEGA},{'o'}, {'a'}, {'v'}, {'w'},

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
        // Set-related
        {'{'}, {'}'}, {','},
        // Dot
        {'.'},
        // Complex
        {'i'}
    };

    public static Dictionary<char, char> symbolTranslation = new Dictionary<char, char>(){
        // Units
        {cLOWER_OMEGA, cUPPER_OMEGA},
        {'o', cUPPER_OMEGA},
        {'a', 'A'},
        {'v', 'V'},
        {'w', 'W'},

        // Operstors
        {'s', '√'},
    };

    static string space = @"\s*";
    public static Dictionary<string, string> unitTranslation = new Dictionary<string, string>(){
        {$@"((A{space}\*?{space}{cUPPER_OMEGA})|({cUPPER_OMEGA}{space}\*?{space}A))", "V"},
        {$@"(W{space}/{space}A)", "V"},
        {$@"((A{space}\*?{space}V)|(V{space}\*?{space}A))", "W"},
        {$@"(V{space}/{space}{cUPPER_OMEGA})", "A"},
        {$@"(W{space}/{space}V)", "A"},
        {$@"(V{space}/{space}A)", sUPPER_OMEGA},
    };



    private static CalculatorController controller;
    private void Start()
    {
        controller = gameObject.AddComponent<CalculatorController>();
        created?.Invoke();
    }


    private static string PrepareExpression(string expression)
    {
        expression = Regex.Replace(expression, @"√(\d*)", @"sqrt($1)");
        expression = expression.Replace("m", "*(10^-3)");
        expression = expression.Replace("k", "*(10^3)");
        return expression;
    }
    
    public static void Solve(string expression)
    {
        string result = "";
        expression = PrepareExpression(expression);

        if (expression.Contains("?")) expression = expression.Replace('?', 'x');
        else expression += " = x";

        //TODO: check if it follows the pattern for an equation
        Entity.Set solutionSet = expression.Solve("x");
        foreach (var solution in (Entity.Set.FiniteSet)solutionSet)
        {
            result += " ";
            if (solution.EvaluableNumerical && solution.EvalNumerical() is Entity.Number.Real realNumber)
            {
                float val = (float)realNumber;
                result += val.ToString("0.00");
            }
            // unknown; could be symbols, complex numbers, etc...
            else
            {
                string t = solution.Simplify().Stringize().Replace(" ", string.Empty);
                result += t;
            }
        }

        result = result.Trim();
        result = result.Replace(" ", ", ");
        // If solution set contains multiple elements
        if (result.Contains(',')) result = "{ " + result + " }";

        foreach (var pair in unitTranslation)
        {
            result = Regex.Replace(result, pair.Key, pair.Value);
        }

        // remove the '*' between numbers and units.
        result = Regex.Replace(result, $@"(\d*){space}(m|k)?{space}\*{space}(V|W|A|{sUPPER_OMEGA})", $@"$1$2$3");

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

    public static bool IsFocused(){
        if(controller == null)
            return false;
        return controller.IsFocused();
    }
}
