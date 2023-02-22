using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogs
{
    public class Dialog : Singleton<Dialog>
    {

        public static Action<DialogEntry> entryStarted;
        public static Action<DialogEntry> entryEnded;

        public static Action<DialogSequence> sequenceStarted;
        public static Action<DialogSequence> sequenceEnded;

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
            RegisterCallbacks();
        }

        static private void RegisterCallbacks()
        {
            image.RegisterCallback<ClickEvent>(Continue);
            textContent.RegisterCallback<ClickEvent>(Continue);
        }

        static private void UnregisterCallbacks()
        {
            image.UnregisterCallback<ClickEvent>(Continue);
            textContent.UnregisterCallback<ClickEvent>(Continue);
        }

        private static void SetImageSprite(Sprite sprite)
        {
            image.style.backgroundImage = new StyleBackground(sprite);
        }

        private static void SetText(string text)
        {
            textContent.text = text;
        }

        public static void PlaySequence(DialogSequence seq)
        {
            container.SetEnabled(true);
            container.visible = true;
            currentSequence = seq;
            sequenceStarted?.Invoke(seq);
            Continue();
        }

        public static void End()
        {
            Debug.Log("End of dialog reached.");
            container.SetEnabled(false);
            container.visible = false;
            sequenceEnded?.Invoke(currentSequence);
            currentSequence = null;
        }

        private static bool paused;
        public static void Pause()
        {
            paused = true;
            UnregisterCallbacks();
        }

        public static void Continue(ClickEvent e = null)
        {
            if (paused) { RegisterCallbacks(); paused = false; }
            currentSequence.Next();
        }

        public static void SetCurrent(DialogEntry dialogEntry)
        {
            SetImageSprite(dialogEntry);
            SetText(dialogEntry);
        }
    }
}