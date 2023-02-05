using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Live : GameModes
    {
        public override void HandleNormal()
        {
            InteractionMode.ChangeTo(InteractionModes.Tweak);

            Clipboard.SetContollerEnabled(false);
            ComponentManager.SetControllersEnabled(false);
            Selection.SetContollerEnabled(false);
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
            InteractionMode.ChangeTo(InteractionModes.Tweak);

            Clipboard.SetContollerEnabled(false);
            ComponentManager.SetControllersEnabled(false);
            Selection.SetContollerEnabled(false);
        }

        public override void HandleWire()
        {
            Debug.Log("Cannot wire in live mode.");
        }
    }
}
