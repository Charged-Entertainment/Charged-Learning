using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace LevelTree
{
    public class LevelButtons : MonoBehaviour
    {
        public List<Button> levelButtons { get; private set; }
        private void Awake()
        {
            levelButtons = UI.GetRootVisualElement().Query<Button>(className: "available").ToList();
        }

        private void OnEnable()
        {
            foreach (var button in levelButtons) button.RegisterCallback<MouseUpEvent>(HandleClick);
        }

        private void OnDisable()
        {
            foreach (var button in levelButtons) button.UnregisterCallback<MouseUpEvent>(HandleClick);
        }

        private void HandleClick(MouseUpEvent e)
        {
            VisualElement ve = (VisualElement)e.target;
            short n = short.Parse(ve.name.Split('-')[1]);
            LevelManager.LevelToLoadOnPuzzleSceneEnter = n;
            SceneManager.LoadScene("PuzzleScene");
        }
    }
}
