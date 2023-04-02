using System.Linq;
using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
[InitializeOnLoad]
public class SyncSelection
{
    static SyncSelection() { EditorApplication.playModeStateChanged += HandlePlayMode; }
    static void HandlePlayMode(PlayModeStateChange mode)
    {
        if (mode == PlayModeStateChange.EnteredPlayMode) Enable();
        else Disable();
    }

    static void Enable()
    {
        Disable();
        // UnityEditor.Selection.selectionChanged += SyncToGame;
        Selection.selectionChanged += SyncFromGame;
    }

    static void Disable()
    {
        // UnityEditor.Selection.selectionChanged -= SyncToGame;
        Selection.selectionChanged -= SyncFromGame;
    }

    static void SyncFromGame(EditorBehaviour c) { SyncFromGame(); }
    static void SyncFromGame()
    {
        HashSet<EditorBehaviour> selection = Selection.GetSelectedComponents<EditorBehaviour>().ToHashSet();
        UnityEditor.Selection.objects = null;
        if (selection.Count == 0) return;
        UnityEditor.Selection.objects = selection.Select(e => e.gameObject).ToArray();
    }
    // void SyncToGame() { /* WiP */ }
}
#endif
