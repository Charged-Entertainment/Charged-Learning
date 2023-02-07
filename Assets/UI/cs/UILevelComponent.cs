using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Components;

public class UILevelComponent: VisualElement
{
    VisualElement instance;
    Label label;
    Image image;
    Label qty;

    LevelComponent levelComponent;

    public UILevelComponent(LevelComponent c)
    {
        levelComponent = c;
        var document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
        var container = document.Q("level-components");
        VisualTreeAsset template = Resources.Load<VisualTreeAsset>("LevelComponent.uxml");
        instance = template.Instantiate();
        label = instance.Q<Label>("name");
        image = instance.Q<Image>();
        qty = instance.Q<Label>("qty");

        label.text = c.Component.Name;
        qty.text = c.Quantity.ToString();
        image.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("led"));
    }

    private void OnEnable()
    {
        OnDisable();
        Puzzle.quantityChanged += HandleQtyChange;
    }

    private void OnDisable()
    {
        Puzzle.quantityChanged -= HandleQtyChange;
    }

    private void HandleQtyChange(LevelComponent c)
    {
        qty.text = c.Quantity.ToString();
    }

    private class UILevelComponentLoader : Singleton<UILevelComponentLoader>
    {
        private void Start()
        {
            var document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
            var container = document.Q("level-components");
            foreach (var c in Puzzle.Components)
            {
                container.Insert(1, new UILevelComponent(c));
            }
        }
    }
}
