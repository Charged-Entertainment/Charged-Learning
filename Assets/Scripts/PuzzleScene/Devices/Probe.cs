using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Probe : MonoBehaviour
{
    public Terminal propedTerminal { get; private set; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var gameObject = other.gameObject;
        if (other.gameObject.tag == "Terminal")
        {
            propedTerminal = other.GetComponent<Terminal>();
            // Debug.Log($"Trigger2d with {propedTerminal} on {propedTerminal.gameObject.transform.parent}");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        propedTerminal = null;
    }
}
