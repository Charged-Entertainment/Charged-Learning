using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Tweak : InteractionModes
    {
        public override void HandleEdit()
        {
            Debug.Log("Cannot tweak in edit mode.");
        }

        public override void HandleEvaluate()
        {
            Debug.Log("Cannot tweak in evaluate mode.");
        }

        public override void HandleLive()
        {
            InteractionMode.ChangeTo(InteractionModes.Tweak);
        }
    }
}
