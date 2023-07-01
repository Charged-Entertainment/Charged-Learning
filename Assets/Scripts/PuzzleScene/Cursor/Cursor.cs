using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public class Cursor : Singleton<Cursor>
{
    static Texture2D normalCursor;
    static Texture2D panCursor;
    static Texture2D liveCursor;
    static Texture2D dragCursor;
    static Texture2D currentCursor;
    
    
    static Texture2D lastSeenCursorBeforeDrag;

    private void Start()
    {
        normalCursor = (Texture2D)Resources.Load("Sprites/Cursors/normal");
        panCursor = (Texture2D)Resources.Load("Sprites/Cursors/pan");
        liveCursor = (Texture2D)Resources.Load("Sprites/Cursors/live");
        dragCursor = (Texture2D)Resources.Load("Sprites/Cursors/drag");

        InteractionMode.changed += HandleInteractionModeChange;
        LiveComponent.mouseDown += HandleMouseDown;
        LiveComponent.mouseUp += HandleMouseUp;

        ChangeCursor(normalCursor);
    }

    static void HandleMouseDown(EditorBehaviour c)
    {
        lastSeenCursorBeforeDrag = currentCursor;
        if(InteractionMode.Current is not GameManagement.Normal) return;
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
        else if (im == InteractionModes.Tweak) ChangeCursor(liveCursor);
        else Debug.Log("Error.");
    }

    static void ChangeCursor(Texture2D newCursor)
    {
        UnityEngine.Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = newCursor;
    }
}
