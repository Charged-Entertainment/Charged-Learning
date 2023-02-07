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

    static public void Copy(ComponentBehavior[] components, bool isCut = false)
    {
        Clear();
        Bounds bounds = Utils.GetBoundsOfComponentsArray(components);

        Instance.transform.position = bounds.center;

        foreach (var component in components)
        {
            ComponentBehavior copy = ComponentManager.Instantiate(component, Instance.transform);
            copy.transform.position -= Instance.transform.position;
            copy.Disable();

            if (isCut) GameObject.Destroy(component);
        }
    }

    static public void Paste(Vector2 pos)
    {
        Instance.transform.position = pos;

        foreach (var component in GetContent())
        {
            component.Enable();
            component.gameObject.transform.parent = null;
        }

        Instance.transform.position = Vector3.zero;
    }

    static public ComponentBehavior[] GetContent()
    {
        return Instance.GetComponentsInChildren<ComponentBehavior>(true);
    }

    static public void Clear()
    {
        foreach (var component in GetContent())
        {
            component.Destroy();
        }
    }
}
