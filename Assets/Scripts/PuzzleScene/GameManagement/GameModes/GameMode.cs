using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class GameMode : StateMachine<GameModes>
    {
        public static void Normal() { Current.HandleNormal(); }
        public static void Pan() { Current.HandlePan(); }
        public static void Tweak() { Current.HandleTweak(); }

        private void Start() {
            ChangeTo(GameModes.Edit);
        }
    }

    abstract public class GameModes : State
    {
        public abstract void HandleNormal();
        public abstract void HandlePan();
        public abstract void HandleTweak();
        public static Edit Edit = new Edit();
        public static Live Live = new Live();
        public static Evaluate Evaluate = new Evaluate();
    }

}
