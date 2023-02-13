using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogs
{
    public class Dialog : Singleton<Dialog>
    {

        private static VisualElement container;

        private static DialogSequence currentSequence;

        private static Image image;
        private static Label textContent;

        private new void Awake()
        {
            base.Awake();
            container = GameObject.Find("UIDocument").GetComponent<UIDocument>().rootVisualElement.Q("dialog");
            image = container.Q<Image>();
            textContent = container.Q<Label>();

            image.RegisterCallback<ClickEvent>(e=> PlayNextEntry());
            textContent.RegisterCallback<ClickEvent>(e=> PlayNextEntry());
        }

        private static void SetImageSprite(Sprite sprite) {
            image.style.backgroundImage = new StyleBackground(sprite);
        }

        private static void SetText(string text) {
            textContent.text = text;
        }

        public static void PlaySequence(DialogSequence seq)
        {
            container.SetEnabled(true);
            container.visible = true;
            currentSequence = seq;
            PlayNextEntry();
        }

        public static void End()
        {
            Debug.Log("End of dialog reached.");
            container.SetEnabled(false);
            container.visible = false;
            currentSequence = null;
        }

        private static void PlayNextEntry() {
            currentSequence.Next();
        }

        public static void SetCurrent(DialogEntry dialogEntry) {
            SetImageSprite(dialogEntry);
            SetText(dialogEntry);
        }
    }
}