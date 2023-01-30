using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        GameMode.InteractionModeChanged += HandleInteractionModeChange;
        EComponent.mouseDown += HandleMouseDown;
        EComponent.mouseUp += HandleMouseUp;

        ChangeCursor(normalCursor);
    }

    static void HandleMouseDown(ComponentBehavior c)
    {
        lastSeenCursorBeforeDrag = currentCursor;
        ChangeCursor(dragCursor);
    }

    static void HandleMouseUp(ComponentBehavior c)
    {
        ChangeCursor(lastSeenCursorBeforeDrag);
    }

    static void HandleInteractionModeChange(GameMode.InteractionMode mode)
    {
        if (mode == GameMode.InteractionMode.Normal) ChangeCursor(normalCursor);
        else if (mode == GameMode.InteractionMode.Pan) ChangeCursor(panCursor);
        else if (mode == GameMode.InteractionMode.Wire) ChangeCursor(wireCursor);
        else if (mode == GameMode.InteractionMode.Tweak) ChangeCursor(wireCursor);
        else Debug.Log("Error.");
    }

    static void ChangeCursor(Texture2D newCursor)
    {
        UnityEngine.Cursor.SetCursor(newCursor, Vector2.zero, CursorMode.Auto);
        currentCursor = newCursor;
    }
}
