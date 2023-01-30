using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedoController : Controller
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && Input.GetKey(KeyCode.LeftControl)){
            Debug.Log("undo keys pressed");
            mainManager.undoRedoManager.Undo();
        }
        if (Input.GetKeyDown(KeyCode.N) && Input.GetKey(KeyCode.LeftControl)){
            Debug.Log("Redo keys pressed");
            mainManager.undoRedoManager.Redo();
        }


    }
}
