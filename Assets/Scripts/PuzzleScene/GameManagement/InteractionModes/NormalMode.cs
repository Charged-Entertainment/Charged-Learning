using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Normal : InteractionModes
    {
        public override void HandleEdit()
        {
            InteractionMode.ChangeTo(InteractionModes.Normal);
        }

        public override void HandleEvaluate()
        {
            InteractionMode.ChangeTo(InteractionModes.Normal);
        }

        public override void HandleLive()
        {
            InteractionMode.ChangeTo(InteractionModes.Normal);
        }
    }
}
