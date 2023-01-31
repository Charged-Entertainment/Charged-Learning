using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMode : Singleton<GameMode>
{
    public partial class Controller : Singleton<Controller>
    {
        private void Update()
        {
            // handle game modes
            if (Input.GetKeyDown(KeyCode.Z)) GameMode.SetGameMode<Edit>();
            else if (Input.GetKeyDown(KeyCode.X)) GameMode.SetGameMode<Live>();
            else if (Input.GetKeyDown(KeyCode.C)) GameMode.SetGameMode<Evaluate>();
            
            // handle interaction modes
            else if (Input.GetKeyDown(KeyCode.Q)) GameMode.SetInteractionModeToNormal();
            else if (Input.GetKeyDown(KeyCode.W)) GameMode.SetInteractionModeToWire();
            else if (Input.GetKeyDown(KeyCode.E)) GameMode.SetInteractionModeToPan();
            else if (Input.GetKeyDown(KeyCode.R)) GameMode.SetInteractionModeToTweak();
        }
    }
}
