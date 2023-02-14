using System.Linq;
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
                Clipboard.Copy(Selection.GetSelectedComponents<LiveComponent>());
            }

            else if (Input.GetKeyDown(KeyCode.V) && Input.GetKey(KeyCode.LeftControl))
            {
                Clipboard.Paste(Utils.GetMouseWorldPosition());
            }
        }
    }
}
