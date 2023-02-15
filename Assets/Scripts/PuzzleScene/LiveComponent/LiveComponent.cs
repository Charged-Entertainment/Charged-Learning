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

    [field:SerializeField] public Terminal[] Terminals{get; private set;}

    protected override void Awake()
    {
        Terminals = gameObject.GetComponentsInChildren<Terminal>(true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        destroyed?.Invoke(this);
    }

    // Handle all that should happen when creating a new component.
    static public LiveComponent Instantiate(Components.LevelComponent comp, Transform parent = null, Vector2? pos = null)
    {
        if (comp.Quantity.Used < comp.Quantity.Total)
        {
            var prefab = Resources.Load<GameObject>($"Components/{comp.Name}");
            LiveComponent copy = GameObject.Instantiate(prefab, parent).GetComponent<LiveComponent>();
            copy.levelComponent = comp;
            if (pos != null) copy.transform.position = (Vector2)pos;
            created?.Invoke(copy);
            return copy;
        }
        else
        {
            throw new Exception("Quantity...");
        }
    }
}
