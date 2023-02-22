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
            SimulationManager.Simulate();
        }

        public override void HandlePan()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);
        }

        public override void HandleTweak()
        {
            Debug.Log("Cannot set interaction mode to tweak in edit game mode.");
        }
    }
}
