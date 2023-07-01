using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace LevelTree
{
    public class BackButton : MonoBehaviour
    {
        public Button button { get; private set; }
        private void Awake()
        {
            button = UI.GetRootVisualElement().Q<Button>("back-btn");
        }

        private void OnEnable()
        {
            Debug.Log("Back button enabled");
            button.RegisterCallback<MouseUpEvent>(HandleClick);
        }

        private void OnDisable()
        {
            Debug.Log("Back button disabled");
            button.UnregisterCallback<MouseUpEvent>(HandleClick);
        }

        private void HandleClick(MouseUpEvent e)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}