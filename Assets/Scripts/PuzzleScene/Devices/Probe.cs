using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Probe : EditorBehaviour
{
    public override void Destroy() {
        Debug.Log("Cannot delete multimeter probes. Delete the whole multimeter instead.");
    }
}
