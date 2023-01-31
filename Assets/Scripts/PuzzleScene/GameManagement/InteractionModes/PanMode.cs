using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Pan : InteractionModes
    {
        public override void HandleEdit()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);
        }

        public override void HandleEvaluate()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);
        }

        public override void HandleLive()
        {
            InteractionMode.ChangeTo(InteractionModes.Pan);
        }
    }
}
