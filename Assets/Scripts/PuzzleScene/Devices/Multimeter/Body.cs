using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : EditorBehaviour
{
    public override void Destroy()
    {
        Multimeter.Destroy();
    }
}
