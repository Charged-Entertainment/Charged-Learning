using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public partial class GameManager : Singleton<GameManager>
{
    private partial class GameController : Controller
    {
        private void Update()
        {
            // handle game modes
            if (Input.GetKeyDown(KeyCode.Alpha1)) GameMode.ChangeTo(GameModes.Edit);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) GameMode.ChangeTo(GameModes.Live);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) GameMode.ChangeTo(GameModes.Evaluate);
            
            // handle interaction modes
            else if (Input.GetKeyDown(KeyCode.Q)) GameMode.Normal();
            else if (Input.GetKeyDown(KeyCode.W)) GameMode.Wire();
            else if (Input.GetKeyDown(KeyCode.E)) GameMode.Pan();
            else if (Input.GetKeyDown(KeyCode.R)) GameMode.Tweak();
        }
    }
}
