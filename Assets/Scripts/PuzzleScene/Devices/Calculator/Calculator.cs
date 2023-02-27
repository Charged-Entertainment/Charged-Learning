using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Symbolism;
using Symbolism.IsolateVariable;
using AngouriMath;

public class Calculator : MonoBehaviour
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
        {'+'}, {'-'}, {'*'}, {'/'}, {'^'}, {'s'},

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



    private static CalculatorController controller;
    private void Awake()
    {
        controller = gameObject.AddComponent<CalculatorController>();
    }


    private static string PrepareExpression(string expression){
        expression = expression.Replace("√", "sqrt");
        return expression;
    }
    public static void Solve(string expression)
    {
        expression = PrepareExpression(expression);
        if (expression.Contains("?"))
        {
            //TODO: check if it follows the pattern for an equation
            expression = expression.Replace('?','x');
            Entity equation = expression;
            controller.Display(equation.Solve("x").Stringize());
        }else{
            Entity equation = expression;
            controller.Display(((double)equation.EvalNumerical()).ToString());
        }
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
}
