using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private byte CurrentLevel = 1;
    void Start()
    {
        if (CurrentLevel == 1) gameObject.AddComponent<Level1>();
        else if (CurrentLevel == 2) gameObject.AddComponent<Level2>();
        else Debug.Log($"Unknown level: #{CurrentLevel}");
    }
}
