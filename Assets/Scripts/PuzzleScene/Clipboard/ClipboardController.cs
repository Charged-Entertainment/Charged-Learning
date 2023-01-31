using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Clipboard : Singleton<Clipboard>
{
    private class ClipboardController : Controller
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl))
            {
                Clipboard.Copy(new List<ComponentBehavior>(Selection.GetSelectedComponents()).ToArray());
            }

            else if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl))
            {
                Clipboard.Paste(Utils.GetMouseWorldPosition());
            }
        }
    }
}
