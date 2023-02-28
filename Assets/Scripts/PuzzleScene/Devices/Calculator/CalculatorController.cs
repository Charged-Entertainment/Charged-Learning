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

    public void WriteChar(char c){
        inputField.text += c;
    }


    public void Submit(){
        HandleSubmit(inputField.text);
    }
}
