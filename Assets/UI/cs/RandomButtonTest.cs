using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomButtonTest : MonoBehaviour
{
    private void OnEnable() {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        var RandomButtonTest = rootVisualElement.Q("random-component-test");
        RandomButtonTest.RegisterCallback<MouseDownEvent>((ev) => ComponentManager.Instantiate(Puzzle.testComp));


    }
}
