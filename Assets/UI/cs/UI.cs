using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static partial class UI
{
    private static VisualElement document
    {
        get
        {
            return GetRootVisualElement();
        }
    }

    public static VisualElement GetRootVisualElement()
    {
        var _document = GameObject.Find("UIDocument").GetComponent<UIDocument>();
        return _document.rootVisualElement;
    }

    public abstract class UIBaseElement : MonoBehaviour { }
}