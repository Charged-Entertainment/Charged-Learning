using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Components;

public class UILevelComponent : VisualElement
{
    public static Action<UILevelComponent> created;

    public VisualElement visualElement {get; private set;}
    Label label;
    Image image;
    Label qty;

    public LevelComponent levelComponent {get; private set;}

    public UILevelComponent(LevelComponent c)
    {
        levelComponent = c;
        VisualTreeAsset template = Resources.Load<VisualTreeAsset>("LevelComponent");
        visualElement = template.Instantiate();

        label = visualElement.Q<Label>("name");
        image = visualElement.Q<Image>();
        qty = visualElement.Q<Label>("qty");

        label.text = c.Name;
        qty.text = "x" + c.Quantity.Total.ToString();

        var sprite = Resources.Load<Sprite>(c.Name);
        image.style.backgroundImage = new StyleBackground(sprite);

        OnEnable();
    }

    private void OnEnable()
    {
        LevelComponent.quantityChanged += HandleQtyChange;
        image.RegisterCallback<MouseEnterEvent>(e =>
        {
            image.style.opacity = 0.8f;
        });

        image.RegisterCallback<MouseLeaveEvent>(e =>
        {
            image.style.opacity = 1f;
        });

        image.RegisterCallback<MouseDownEvent>(e =>
        {
            image.style.opacity = 0.5f;
        });

        image.RegisterCallback<MouseUpEvent>(e =>
        {
            image.style.opacity = 1f;
            LiveComponent.Instantiate(levelComponent, pos: Vector2.zero);

            // TODO: fix this
            // ComponentManager.Instantiate(levelComponent, Utils.GetMouseWorldPosition());
        });
    }

    private void HandleQtyChange(LevelComponent c)
    {
        if (c == levelComponent)
        {
            visualElement.Q<Label>("qty").text = "x" + (c.Quantity.Total - c.Quantity.Used).ToString();
            if (c.Quantity.Used == c.Quantity.Total) visualElement.style.opacity = 0.3f;
            else visualElement.style.opacity = 1f;
        }
    }

    private class UILevelComponentLoader : Singleton<UILevelComponentLoader>
    {
        private void Start()
        {
            var document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
            var container = document.Q("level-components");
            foreach (var c in Puzzle.Components)
            {
                var t = new UILevelComponent(c);
                container.Add(t.visualElement);
                UILevelComponent.created?.Invoke(t);
            }
        }
    }
}
