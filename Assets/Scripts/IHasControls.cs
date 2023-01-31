using System.Collections.Generic;
using UnityEngine;
interface IHasControls
{
    public List<Controller> Controllers { get; }

    public void EnableAllControllers()
    {
        foreach (var c in Controllers)
        {
            c.enabled = true;
        }
    }
    public void DisableAllControllers()
    {
        foreach (var c in Controllers)
        {
            c.enabled = false;
        }
    }
    public void EnableController(Controller c)
    {
        c.enabled = true;
    }
    public void DisableController(Controller c)
    {
        c.enabled = false;
    }
}