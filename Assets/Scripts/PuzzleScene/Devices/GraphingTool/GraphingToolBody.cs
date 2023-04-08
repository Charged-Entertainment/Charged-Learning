using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphingToolBody : EditorBehaviour
{
    public override void Destroy()
    {
        GraphingTool.Destroy();
    }
}
