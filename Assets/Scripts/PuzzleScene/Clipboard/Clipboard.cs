using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagement;

public partial class Clipboard : Singleton<Clipboard>
{
    private ClipboardController controller;
    private void Start()
    {
        controller = gameObject.AddComponent<ClipboardController>();
    }

    private void OnEnable()
    {
        OnDisable();
        GameMode.changed += HandleGameModeChange;
        InteractionMode.changed += HandleInteractionModeChange;
    }

    private void OnDisable()
    {
        GameMode.changed -= HandleGameModeChange;
        InteractionMode.changed -= HandleInteractionModeChange;
    }

    private void HandleGameModeChange(GameModes mode)
    {
        if (GameMode.Current == GameModes.Edit && InteractionMode.Current == InteractionModes.Normal)
            controller.enabled = true;
        else controller.enabled = false;
    }

    private void HandleInteractionModeChange(InteractionModes mode)
    {
        if (GameMode.Current == GameModes.Edit && InteractionMode.Current == InteractionModes.Normal)
            controller.enabled = true;
        else controller.enabled = false;
    }

    #region Clipboard Operations
    private static void _Copy(IList<LiveComponent> components, bool isCut = false)
    {
        Clear();
        Bounds bounds = Utils.GetBounds<LiveComponent>(components);

        Instance.transform.position = bounds.center;

        foreach (var component in components)
        {
            //filthy solution to allow cutting where qty == 0
            if (isCut) component.levelComponent.Quantity.Used -= components.Count;

            LiveComponent copy = LiveComponent.Instantiate(component.levelComponent, Instance.transform, component.transform.position);
            copy.Disable();

            if (isCut)
            {
                component.Destroy();
                component.levelComponent.Quantity.Used += components.Count;
            }
        }
    }

    private static void _Paste(Vector2 pos)
    {
        Instance.transform.position = pos;

        var content = GetContent();

        Selection.Clear();
        Selection.AddComponents(new List<EditorBehaviour>(content));

        foreach (var component in content)
        {
            component.Enable();
            component.gameObject.transform.parent = null;
        }

        Instance.transform.position = Vector3.zero;
    }

    private static void _Clear()
    {
        foreach (var component in GetContent())
        {
            component.Destroy();
        }
    }

    private static LiveComponent[] _GetContent()
    {
        return Instance.GetComponentsInChildren<LiveComponent>(true);
    }
    #endregion

    #region Clipboard API
    /// <summary>
    /// Add the given list of live components to the clipboard.
    /// </summary>
    static public void Copy(IList<LiveComponent> components) { _Copy(components); }

    /// <summary>
    /// Add the currently selected live components to the clipboard.
    /// </summary>
    static public void Copy() { _Copy(Selection.GetSelectedComponents<LiveComponent>()); }

    /// <summary>
    /// Add the given list of live components to the clipboard and destroy them.
    /// </summary>
    static public void Cut(IList<LiveComponent> components) { _Copy(components, true); }

    /// <summary>
    /// Add the currently selected live components to the clipboard and destroy them.
    /// </summary>
    static public void Cut() { _Copy(Selection.GetSelectedComponents<LiveComponent>(), true); }

    /// <summary>
    /// Paste the content of the clipboard at the cursor's current position.
    /// </summary>
    static public void Paste() { _Paste(Utils.GetMouseWorldPosition()); }

    /// <summary>
    /// Paste the content of the clipboard at the given position.
    /// </summary>
    static public void Paste(Vector2 pos) { _Paste(pos); }

    /// <summary>
    /// Retrieve the content of the clipboard.
    /// </summary>
    static public LiveComponent[] GetContent() { return _GetContent(); }

    /// <summary>
    /// Clear the clipboard.
    /// </summary>
    static public void Clear() { _Clear(); }
    #endregion
}
