using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UILevelComponentsCollapse : MonoBehaviour
{
    VisualElement document, container, header;
    Image image;

    string up = "up-arrow";
    string down = "down-arrow";
    bool isCollapsed = false;

    StyleLength originalHeight;

    void Start()
    {
        document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;

        container = document.Q("components-bar").Q("level-components");
        header = document.Q("components-bar").Q("components-bar-header");
        image = header.Q<Image>();

        header.RegisterCallback<ClickEvent>(ToggleCollapse);

        originalHeight = container.style.height;
    }

    private void ToggleCollapse(ClickEvent ev)
    {
        isCollapsed = !isCollapsed;
        image.EnableInClassList(up, !isCollapsed);
        image.EnableInClassList(down, isCollapsed);
        container.style.height = isCollapsed ? 0 : originalHeight;
        
        container.SetEnabled(!isCollapsed);
        container.visible = !isCollapsed;
    }
}
