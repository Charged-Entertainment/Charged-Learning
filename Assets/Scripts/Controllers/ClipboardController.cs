using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Controllers
{
    public class ClipboardController : Controller<ClipboardController>
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
