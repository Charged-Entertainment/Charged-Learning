using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameMode : Singleton<GameMode>
{
    public static Action<Type> GameModeChanged;
    public static Action<InteractionMode> InteractionModeChanged;
    public static Type CurrentGameMode { get; private set; }

    public enum InteractionMode
    {
        Normal,
        Pan,
        Wire,
        Tweak
    }

    public static InteractionMode CurrentInteractionMode { get; private set; }

    public static void SetInteractionMode(InteractionMode newMode)
    {
        if (newMode == InteractionMode.Normal)
        {

        }
        else if (newMode == InteractionMode.Pan)
        {

        }
        else if (newMode == InteractionMode.Wire)
        {

        }
        else if (newMode == InteractionMode.Tweak)
        {

        }

        CurrentInteractionMode = newMode;
        InteractionModeChanged.Invoke(newMode);
    }

    private static void SetNormalInteractionMode()
    {
        if (CurrentGameMode == typeof(Edit))
        {
            CurrentInteractionMode = InteractionMode.Normal;
            InteractionModeChanged.Invoke(InteractionMode.Normal);
            Selection.Controller.Enable();
        }
        else if (CurrentGameMode == typeof(Live))
        {
            CurrentInteractionMode = InteractionMode.Tweak;
            InteractionModeChanged.Invoke(InteractionMode.Tweak);
            // Enable tweak stuff
        }
        else if (CurrentGameMode == typeof(Evaluate))
        {
            Debug.Log("Cannot set interaction mode to normal in evaluate game mode.");
        }
        else Debug.Log("Error (unknown CurrentGameMode type): " + CurrentGameMode.Name);
    }

    private static void SetPanInteractionMode()
    {
        if (CurrentGameMode == typeof(Edit))
        {
            CurrentInteractionMode = InteractionMode.Pan;
            InteractionModeChanged.Invoke(InteractionMode.Pan);
            Selection.Controller.Disable();
            // Wiring.Controller.Disable();
        }
        else if (CurrentGameMode == typeof(Live))
        {
            CurrentInteractionMode = InteractionMode.Pan;
            InteractionModeChanged.Invoke(InteractionMode.Pan);
            // Disable tweak stuff
        }
        else if (CurrentGameMode == typeof(Evaluate))
        {
            Debug.Log("Should already be in pan interaction mode in evaluate game mode.");
        }
        else Debug.Log("Error (unknown CurrentGameMode type): " + CurrentGameMode.Name);
    }

    private static void SetWireInteractionMode()
    {
        if (CurrentGameMode == typeof(Edit))
        {
            CurrentInteractionMode = InteractionMode.Wire;
            InteractionModeChanged.Invoke(InteractionMode.Wire);
            Selection.Controller.Disable();
        }
        else if (CurrentGameMode == typeof(Live))
        {
            Debug.Log("Cannot set interaction mode to wire in live game mode.");
        }
        else if (CurrentGameMode == typeof(Evaluate))
        {
            Debug.Log("Cannot set interaction mode to wire in evaluate game mode.");
        }
        else Debug.Log("Error (unknown CurrentGameMode type): " + CurrentGameMode.Name);
    }

    private static void SetTweakInteractionMode()
    {
        if (CurrentGameMode == typeof(Edit))
        {
            Debug.Log("Cannot set interaction mode to tweak in edit game mode.");
        }
        else if (CurrentGameMode == typeof(Live))
        {
            CurrentInteractionMode = InteractionMode.Wire;
            InteractionModeChanged.Invoke(InteractionMode.Wire);
        }
        else if (CurrentGameMode == typeof(Evaluate))
        {
            Debug.Log("Cannot set interaction mode to tweak in evaluate game mode.");
            // Cannot be done.
        }
        else Debug.Log("Error (unknown CurrentGameMode type): " + CurrentGameMode.Name);
    }

    private void Start()
    {
        CurrentGameMode = typeof(Edit);
        CurrentInteractionMode = InteractionMode.Normal;
    }

    public static void SetGameMode<T>()
    {
        if (typeof(T) != typeof(Edit) && typeof(T) != typeof(Live) && typeof(T) != typeof(Evaluate))
        {
            Debug.Log("Error, cannot set unkown game mode: " + typeof(T));
            return;
        }
        CurrentGameMode = typeof(T);
        GameModeChanged?.Invoke(typeof(T));

        if (typeof(T) == typeof(Edit))
        {
            SetInteractionMode(InteractionMode.Normal);
            Selection.Controller.Enable();
        }
        else if (typeof(T) == typeof(Live))
        {
            SetInteractionMode(InteractionMode.Tweak);
            Selection.Controller.Disable();
        }
        else if (typeof(T) == typeof(Evaluate))
        {
            SetInteractionMode(InteractionMode.Pan);
            Selection.Controller.Disable();
        }
        Debug.Log($"Now in {typeof(T).Name} game mode.");
    }

}
