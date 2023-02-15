using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Probe : EditorBehaviour
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

    public override void Destroy() {
        Debug.Log("Cannot delete multimeter probes. Delete the whole multimeter instead.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        propedTerminal = null;
    }
}
