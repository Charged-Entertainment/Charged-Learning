using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UI
{
    public static VisualElement LevelComponentsHeader;
    public class UILevelComponentsCollapse : UIBaseElement
    {
        static VisualElement container;
        static Image image;
        static string up = "up-arrow";
        static string down = "down-arrow";
        static bool isCollapsed = false;
        static StyleLength originalHeight;

        void Start()
        {
            container = document.Q("components-bar").Q("level-components");
            LevelComponentsHeader = document.Q("components-bar").Q("components-bar-header");
            image = LevelComponentsHeader.Q<Image>();
            LevelComponentsHeader.RegisterCallback<ClickEvent>(Toggle);
            originalHeight = container.style.height;
        }
        public static void Toggle()
        {
            Toggle(null);
        }
        public static void Toggle(ClickEvent ev)
        {
            isCollapsed = !isCollapsed;
            image.EnableInClassList(up, !isCollapsed);
            image.EnableInClassList(down, isCollapsed);
            container.style.height = isCollapsed ? 0 : originalHeight;

            container.SetEnabled(!isCollapsed);
            container.visible = !isCollapsed;
        }
    }
}