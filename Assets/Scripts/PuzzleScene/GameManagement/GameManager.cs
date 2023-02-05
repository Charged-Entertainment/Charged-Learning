using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public partial class GameManager : Singleton<GameManager>
{
    private GameController gameController;

    void Start()
    {
        gameController = gameObject.AddComponent<GameController>();
    }

    private void OnEnable() {
        OnDisable();
        GameMode.changed += SetToNormalInteraction;
    }

    private void OnDisable() {
        GameMode.changed -= SetToNormalInteraction;
    }

    private static void SetToNormalInteraction(GameModes mode) {
        GameMode.Normal();
    }

    static public void SetContollerEnabled(bool enabled) {
        Instance.gameController.enabled = enabled;
    }
}
