using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMode : Singleton<GameMode>
{
    public partial class Controller: Singleton<Controller>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z)) GameMode.SetGameMode<Edit>();
            else if (Input.GetKeyDown(KeyCode.X)) GameMode.SetGameMode<Live>();
            else if (Input.GetKeyDown(KeyCode.C)) GameMode.SetGameMode<Evaluate>();
            // else if (GameMode.CurrentGameMode == typeof(GameMode.Edit)) HandleEditMode();
            HandleEditMode();
        }

        public void HandleEditMode()
        {
            if (Input.GetKeyDown(KeyCode.Q)) GameMode.SetNormalInteractionMode();
            else if (Input.GetKeyDown(KeyCode.W)) GameMode.SetWireInteractionMode();
            else if (Input.GetKeyDown(KeyCode.E)) GameMode.SetPanInteractionMode();
        }

        public void HandleLiveMode()
        {

        }

        public void HandleEvaluateMode()
        {

        }
    }
}
