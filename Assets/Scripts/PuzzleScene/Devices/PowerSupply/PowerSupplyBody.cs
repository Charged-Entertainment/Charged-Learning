using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyBody : EditorBehaviour
{
    public override void Destroy()
    {
        PowerSupply.Destroy();
    }
}
