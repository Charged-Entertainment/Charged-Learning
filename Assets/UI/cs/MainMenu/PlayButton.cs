using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public Button button { get; private set; }
        private void Awake()
        {
            button = UI.GetRootVisualElement().Q<Button>("play-btn");
        }

        private void OnEnable()
        {
            Debug.Log("Play button enabled");
            button.RegisterCallback<MouseUpEvent>(HandleClick);
        }

        private void OnDisable()
        {
            Debug.Log("Play button disabled");
            button.UnregisterCallback<MouseUpEvent>(HandleClick);
        }

        private void HandleClick(MouseUpEvent e)
        {
            SceneManager.LoadScene("PuzzleScene");
        }
    }
}