using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private Text textElement;

    private void Awake()
    {
        textElement = gameObject.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        textElement.text = "";
        AppendChar(" ");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && textElement.text.Length > 1) RemoveLastChar();
        bool pressed = Calculator.SupportedSymbols.Any(e => Input.GetKeyDown(e.Key));
        if (pressed)
        {
            AppendChar(Calculator.SupportedSymbols.First(e => Input.GetKeyDown(e.Key)).Value);
        }

    }

    void RemoveLast() {
        if (textElement.text.Length <= 0) return;
        textElement.text = textElement.text.Remove(textElement.text.Length - 1);
    }

    void RemoveLastChar() {
        RemoveCursor();
        RemoveLast();
        AddCursor();
    }

    void RemoveCursor()
    {
        if (textElement.text.Length >= 1 && textElement.text.Last() == '|') RemoveLast();
    }

    void AddCursor()
    {
        if (textElement.text.Length >= 1 && textElement.text.Last() != '|')  textElement.text += "|";
    }

    void AppendChar(string s) {
        RemoveCursor();
        textElement.text += s;
        AddCursor();
    }
}
