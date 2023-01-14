using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardController : Controller
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl))
        {
            mainManager.clipboardManager.Copy(new List<ComponentBehavior>(mainManager.selectionManager.GetSelectedComponents()).ToArray());
        }

        else if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl))
        {
            mainManager.clipboardManager.Paste(Utils.GetMouseWorldPosition());
        }
    }
}
