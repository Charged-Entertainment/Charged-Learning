using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Evaluate : GameModes
    {
        public override void HandleNormal()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);

            Clipboard.SetContollerEnabled(false);
            EComponent.SetControllersEnabled(false);
            Selection.SetContollerEnabled(false);
        }

        public override void HandlePan()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);

            Clipboard.SetContollerEnabled(false);
            EComponent.SetControllersEnabled(false);
            Selection.SetContollerEnabled(false);
        }

        public override void HandleTweak()
        {
            Debug.Log("Cannot set interaction mode to tweak in edit game mode.");
        }

        public override void HandleWire()
        {
            Debug.Log("Cannot set interaction mode to wire in evaluate game mode.");
        }
    }
}
