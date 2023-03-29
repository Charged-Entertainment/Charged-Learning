using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    private static Background instance;
    [SerializeField] private RawImage image;
    [SerializeField] private float x, y;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else GameObject.Destroy(gameObject);
    }

    private void OnEnable() {
        SceneManager.activeSceneChanged += HandleSceneChanged;
    }

    void HandleSceneChanged(Scene from, Scene to) {
        if (to.name == "PuzzleScene") Destroy(gameObject);
    }

    private void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }
}
