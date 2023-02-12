using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class LiveComponent : MonoBehaviour
{
    public Components.LevelComponent levelComponent;

    private Terminal[] terminals;

    void Start()
    {
        terminals = gameObject.GetComponentsInChildren<Terminal>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
