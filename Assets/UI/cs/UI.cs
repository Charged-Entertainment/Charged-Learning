using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static partial class UI
{
    private static UIDocument _document = null;
    private static VisualElement document = GetRootVisualElement();

    public static VisualElement GetRootVisualElement()
    {
        if (_document == null) _document = GameObject.Find("UIDocument").GetComponent<UIDocument>();
        if (document == null) document = _document.rootVisualElement;
        return document;
    }

    public abstract class UIBaseElement : MonoBehaviour { }
}