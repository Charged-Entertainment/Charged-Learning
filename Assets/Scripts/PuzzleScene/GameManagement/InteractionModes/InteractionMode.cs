using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class InteractionMode : StateMachine<InteractionModes>
    {
    }

    abstract public class InteractionModes : State
    {
        public static Normal Normal = new Normal();
        public static Pan Pan = new Pan();
        public static Wire Wire = new Wire();
        public static Tweak Tweak = new Tweak();
    }

}
