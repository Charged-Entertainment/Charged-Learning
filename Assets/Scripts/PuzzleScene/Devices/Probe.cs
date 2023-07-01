using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;

public class Probe : EditorBehaviour
{
    public Terminal Terminal {
        get {
            return gameObject.GetComponentInChildren<Terminal>();
        }
    }
    public override void Destroy() {
        FeebackTerminal.Write(new Log("Cannot delete device probes. Delete the whole device instead.", LogType.Warning));
    }
}
