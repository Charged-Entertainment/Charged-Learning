using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Edit : GameModes
    {
        public override void HandleNormal()
        {
            InteractionMode.ChangeTo(InteractionModes.Normal);
            
            Clipboard.SetContollerEnabled(true);
            ComponentManager.SetControllersEnabled(true);
            Selection.SetContollerEnabled(true);
        }

        public override void HandlePan()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);
            
            Clipboard.SetContollerEnabled(false);
            ComponentManager.SetControllersEnabled(false);
            Selection.SetContollerEnabled(false);
        }

        public override void HandleTweak()
        {
            Debug.Log("Cannot set interaction mode to tweak in edit game mode.");
        }

        public override void HandleWire()
        {
            InteractionMode.ChangeTo(InteractionModes.Wire);

            Clipboard.SetContollerEnabled(false);
            ComponentManager.SetControllersEnabled(false);
            Selection.SetContollerEnabled(false);
        }
    }
}
