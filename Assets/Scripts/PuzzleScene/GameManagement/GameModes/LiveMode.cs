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
        }

        public override void HandlePan()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);
        }

        public override void HandleTweak()
        {
            InteractionMode.ChangeTo(InteractionModes.Tweak);
        }
    }
}
