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
    static public Action<LiveComponent> created;
    static public Action<LiveComponent> destroyed;
    static public Action<Terminal, Terminal> connected;
    static public Action<Terminal, Terminal> disconnected;

    public Components.LevelComponent levelComponent;

    [field:SerializeField] public Terminal[] Terminals{get; private set;}

    void Awake()
    {
        Terminals = gameObject.GetComponentsInChildren<Terminal>(true);
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
    public void Destroy()
    {
        LiveComponent.destroyed.Invoke(this);
        GameObject.Destroy(gameObject);
    }
}
