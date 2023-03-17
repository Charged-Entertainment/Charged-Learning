using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] static public short LevelToLoadOnPuzzleSceneEnter = 1;
    void Start()
    {
        if (LevelToLoadOnPuzzleSceneEnter == 1) gameObject.AddComponent<Level1>();
        else if (LevelToLoadOnPuzzleSceneEnter == 2) gameObject.AddComponent<Level2>();
        else if (LevelToLoadOnPuzzleSceneEnter == 3) gameObject.AddComponent<Level3>();
        else
        {
            Debug.Log($"Unknown level: #{LevelToLoadOnPuzzleSceneEnter}");
            Debug.Log($"Adding demo components");

            // for testing in sandbox mode
            var r = Puzzle.CreateLevelComponent("resistor", Components.ComponentType.Resistor, 5);
            var b = Puzzle.CreateLevelComponent("battery", Components.ComponentType.Battery, 5);
            var l = Puzzle.CreateLevelComponent("led", Components.ComponentType.Led, 5);
            Puzzle.AddProperty(r, Components.PropertyType.Resistance, 330);
            Puzzle.AddProperty(b, Components.PropertyType.Voltage, 9);
        }
    }
}
