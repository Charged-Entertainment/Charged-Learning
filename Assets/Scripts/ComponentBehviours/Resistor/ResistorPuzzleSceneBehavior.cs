using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorPuzzleSceneBehavior : MonoBehaviour
{
    Resistor resistor;
    private void Start() {
        resistor = gameObject.GetComponent<Resistor>();
        var c = gameObject.GetComponent<LiveComponent>();
        resistor.SetColorBands((ulong)c.levelComponent.Properties[Components.PropertyType.Resistance].pureProperty.value);
        GameObject.Destroy(this);
    }
}
