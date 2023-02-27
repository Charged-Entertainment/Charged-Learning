using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            Calculator.Solve(inputField.text);
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
}
