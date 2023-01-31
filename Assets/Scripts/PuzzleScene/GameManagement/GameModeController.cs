using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public partial class GameManager : Singleton<GameManager>
{
    public partial class Controller : Singleton<Controller>
    {
        private void Update()
        {
            // handle game modes
            if (Input.GetKeyDown(KeyCode.Z)) GameMode.ChangeTo(GameModes.Edit);
            else if (Input.GetKeyDown(KeyCode.X)) GameMode.ChangeTo(GameModes.Live);
            else if (Input.GetKeyDown(KeyCode.C)) GameMode.ChangeTo(GameModes.Evaluate);
            
            // handle interaction modes
            else if (Input.GetKeyDown(KeyCode.Q)) GameMode.Normal();
            else if (Input.GetKeyDown(KeyCode.W)) GameMode.Wire();
            else if (Input.GetKeyDown(KeyCode.E)) GameMode.Pan();
            else if (Input.GetKeyDown(KeyCode.R)) GameMode.Tweak();
        }
    }
}
