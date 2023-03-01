using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private InputField inputField;

    private void Awake()
    {
        inputField = gameObject.GetComponentInChildren<InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField.onValidateInput += HandleValidation;
        inputField.onSubmit.AddListener(HandleSubmit);
    }

    void HandleSubmit(string s)
    {
        Debug.Log("solve equation");
        Calculator.Solve(s);
        inputField.ActivateInputField();
    }

    char HandleValidation(string input, int charIndex, char addedChar)
    {
        if (Char.IsDigit(addedChar) || Calculator.SymbolSupported(addedChar))
        {
            return Calculator.TranslateSymbol(addedChar);
        }
        return '\0';

    }

    public void Display(string s)
    {
        inputField.text = s;
    }

    public void WriteChar(char c)
    {
        inputField.text = inputField.text.Insert(inputField.caretPosition, c.ToString());
    }


    public void Submit()
    {
        HandleSubmit(inputField.text);
    }

    public void Clear()
    {
        inputField.text = "";
    }

    public void Delete()
    {
        inputField.ProcessEvent(Event.KeyboardEvent("backspace"));
    }

    public void Up()
    {
        inputField.ProcessEvent(Event.KeyboardEvent("up"));
    }

    public void Down()
    {
        inputField.ProcessEvent(Event.KeyboardEvent("down"));

    }

    public void Right()
    {   
        //not working properly
        inputField.ProcessEvent(Event.KeyboardEvent("right"));
    }

    public void Left()
    {
        //not working properly
        inputField.ProcessEvent(Event.KeyboardEvent("left"));
    }
}
