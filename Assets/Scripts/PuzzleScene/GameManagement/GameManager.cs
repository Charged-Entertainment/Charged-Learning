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
        GameMode.changed += Simulate;
    }

    private void OnDisable() {
        GameMode.changed -= SetToNormalInteraction;
        GameMode.changed -= Simulate;
    }

    private static void SetToNormalInteraction(GameModes mode) {
        GameMode.Normal();
    }

    private static void Simulate(GameModes mode = null) {
        if (mode == GameModes.Evaluate || mode == GameModes.Live) SimulationManager.Simulate();
    }

    static public void SetContollerEnabled(bool enabled) {
        Instance.gameController.enabled = enabled;
    }
}
