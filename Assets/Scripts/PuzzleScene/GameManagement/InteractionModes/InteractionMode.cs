using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class InteractionMode : StateMachine<InteractionModes>
    {
        public static void Edit() { Current.HandleEdit(); }
        public static void Live() { Current.HandleLive(); }
        public static void Evaluate() { Current.HandleEvaluate(); }
    }

    abstract public class InteractionModes : State
    {
        public abstract void HandleEdit();
        public abstract void HandleLive();
        public abstract void HandleEvaluate();
        public static Normal Normal = new Normal();
        public static Pan Pan = new Pan();
        public static Wire Wire = new Wire();
        public static Tweak Tweak = new Tweak();
    }

}
