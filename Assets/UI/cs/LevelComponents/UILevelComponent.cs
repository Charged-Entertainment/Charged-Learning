using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Components;

public partial class UI
{
    public class UILevelComponent
    {
        public static Action<UILevelComponent> created;
        // public static float spriteScaleMultiplier = 1f;

        public VisualElement visualElement { get; private set; }
        Image image;
        Label qty;

        public LevelComponent levelComponent { get; private set; }

        public UILevelComponent(LevelComponent c)
        {
            levelComponent = c;
            VisualTreeAsset template = Resources.Load<VisualTreeAsset>("UI/LevelComponent");
            visualElement = template.Instantiate();

            image = visualElement.Q<Image>();
            qty = visualElement.Q<Label>("qty");

            qty.text = "x" + c.Quantity.Total.ToString();

            var sprite = Resources.Load<Sprite>("Sprites/Components/" + c.Name);
            image.style.backgroundImage = new StyleBackground(sprite);
            image.style.height = sprite.rect.height; /* * spriteScaleMultiplier; */
            image.style.width = sprite.rect.width; /* * spriteScaleMultiplier; */

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
            VisualElement document, container;
            private new void Awake()
            {
                base.Awake();
                document = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement;
                container = document.Q("level-components");
                LevelComponent.created += c =>
                {
                    var t = new UILevelComponent(c);
                    container.Add(t.visualElement);
                    UILevelComponent.created?.Invoke(t);
                };
            }
        }
    }
}
