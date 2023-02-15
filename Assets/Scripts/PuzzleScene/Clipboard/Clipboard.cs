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

    static public void Copy(IList<LiveComponent> components, bool isCut = false)
    {
        Clear();
        Bounds bounds = Utils.GetBounds<LiveComponent>(components);

        Instance.transform.position = bounds.center;

        foreach (var component in components)
        {
            //filthy solution to allow cutting where qty == 0
            if (isCut) component.levelComponent.Quantity.Used--; 
            
            LiveComponent copy = LiveComponent.Instantiate(component.levelComponent, Instance.transform, component.transform.position);
            copy.Disable();
            
            if (isCut)
            {
                component.Destroy();
                component.levelComponent.Quantity.Used++;
            }
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

    static public LiveComponent[] GetContent()
    {
        return Instance.GetComponentsInChildren<LiveComponent>(true);
    }

    static public void Clear()
    {
        foreach (var component in GetContent())
        {
            component.Destroy();
        }
    }
}
