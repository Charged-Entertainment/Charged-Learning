using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManagement;
using Components;

public partial class LiveComponent : EditorBehaviour
{
    // Possible change: use UnityEvents (https://www.jacksondunstan.com/articles/3335#comment-713798).
    static new public Action<LiveComponent> created;
    static new public Action<LiveComponent> destroyed;
    static public Action<Terminal, Terminal> connected;
    static public Action<Terminal, Terminal> disconnected;

    public Components.LevelComponent levelComponent;

    private Terminal[] terminals;

    void Start()
    {
        terminals = gameObject.GetComponentsInChildren<Terminal>(true);
    }

    protected override void Awake()
    {
        base.Awake();
        created?.Invoke(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        destroyed?.Invoke(this);
    }


    // Handle all that should happen when creating a new component.
    static public LiveComponent Instantiate(LiveComponent original, Transform parent)
    {
        LiveComponent copy = GameObject.Instantiate(original, parent);

        bool selected = Selection.IsSelected(original);
        if (selected) Selection.AddComponent(copy);

        return copy;
    }

    static public LiveComponent Instantiate(Components.LevelComponent comp)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            var prefab = Resources.Load<GameObject>(comp.Name);
            LiveComponent copy = GameObject.Instantiate(prefab).GetComponent<LiveComponent>();
            var liveComp = copy.gameObject.AddComponent<LiveComponent>();
            liveComp.levelComponent = comp;
            created?.Invoke(copy);
            return copy;
        }
        else
        {
            //TODO: emit error event
            throw new Exception("Quantity ");
        }
    }

    static public LiveComponent Instantiate(Components.LevelComponent comp, Vector2 pos)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            var prefab = Resources.Load<GameObject>($"Components/{comp.Name}");
            LiveComponent copy = GameObject.Instantiate(prefab).GetComponent<LiveComponent>();
            copy.levelComponent = comp;
            copy.transform.position = pos;
            created?.Invoke(copy);
            return copy;
        }
        else
        {
            //TODO: emit error event
            throw new Exception("Quantity ");
        }
    }
}
