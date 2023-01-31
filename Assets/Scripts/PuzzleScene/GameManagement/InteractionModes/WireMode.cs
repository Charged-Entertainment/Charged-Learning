using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class Wire : InteractionModes
    {
        public override void HandleEdit()
        {
            InteractionMode.ChangeTo(InteractionModes.Wire);
        }

        public override void HandleEvaluate()
        {
            Debug.Log("Cannot wire in evaluate mode.");
        }

        public override void HandleLive()
        {
            Debug.Log("Cannot wire in live mode.");
        }
    }
}
