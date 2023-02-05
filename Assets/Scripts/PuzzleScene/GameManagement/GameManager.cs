using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    private GameController gameController;

    void Start()
    {
        gameController = gameObject.AddComponent<GameController>();
    }

    static public void SetContollerEnabled(bool enabled) {
        Instance.gameController.enabled = enabled;
    }
}
