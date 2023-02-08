using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    [field: SerializeField]public ComponentBehavior propedComponent{get; private set;}
    private void OnTriggerEnter2D(Collider2D other) {
        var component = other.GetComponent<ComponentBehavior>();
        if(component){
            Debug.Log($"Trigger2d with{other}");
            propedComponent = component;
        }
            
    }

    private void OnTriggerExit2D(Collider2D other) {
        propedComponent = null;
    }
}
