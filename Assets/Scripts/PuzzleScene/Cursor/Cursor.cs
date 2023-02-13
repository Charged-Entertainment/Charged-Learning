using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public class Cursor : Singleton<Cursor>
{
    static Texture2D normalCursor;
    static Texture2D panCursor;
    static Texture2D wireCursor;
    static Texture2D dragCursor;
    static Texture2D currentCursor;
    
    
    static Texture2D lastSeenCursorBeforeDrag;

    private void Start()
    {
        normalCursor = (Texture2D)Resources.Load("Cursors/normal");
        panCursor = (Texture2D)Resources.Load("Cursors/pan");
        wireCursor = (Texture2D)Resources.Load("Cursors/live");
        dragCursor = (Texture2D)Resources.Load("Cursors/drag");

        InteractionMode.changed += HandleInteractionModeChange;
        LiveComponent.mouseDown += HandleMouseDown;
        LiveComponent.mouseUp += HandleMouseUp;

        ChangeCursor(normalCursor);
    }

    static void HandleMouseDown(EditorBehaviour c)
    {
        lastSeenCursorBeforeDrag = currentCursor;
        ChangeCursor(dragCursor);
    }

    static void HandleMouseUp(EditorBehaviour c)
    {
        ChangeCursor(lastSeenCursorBeforeDrag);
    }

    static void HandleInteractionModeChange(InteractionModes im)
    {
        if (im == InteractionModes.Normal) ChangeCursor(normalCursor);
        else if (im == InteractionModes.Pan) ChangeCursor(panCursor);
        else if (im == InteractionModes.Wire) ChangeCursor(wireCursor);
        else if (im == InteractionModes.Tweak) ChangeCursor(wireCursor);
        else Debug.Log("Error.");
    }

    static void ChangeCursor(Texture2D newCursor)
    {
        UnityEngine.Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = newCursor;
    }
}
