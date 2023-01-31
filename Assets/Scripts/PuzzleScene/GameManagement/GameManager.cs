using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>, IHasControls
{
    public List<Controller> Controllers { get; set; }

    void Start()
    {
        Controllers = new List<Controller>();
        Controllers.Add(gameObject.AddComponent<GameController>());
    }
}
