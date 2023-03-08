using UnityEngine;
using System;

public class ContextMenuManager : Singleton<ContextMenuManager>
{
    [SerializeField] static private ContextMenu contextMenu = null;
    [SerializeField] private ContextMenuController controller;

    private new void Awake() {
        base.Awake();
        controller = gameObject.AddComponent<ContextMenuController>();
    }

    public static void CreateMenu(Type contextMenuType, Vector2 position)
    {
        contextMenu = Instantiate(((GameObject)Resources.Load("Prefabs/ContextMenu/ContextMenu"))).AddComponent(contextMenuType) as ContextMenu;
        contextMenu.transform.position = position;
    }

    public static void Destroy(){
        if(contextMenu == null) return;
        GameObject.Destroy(contextMenu.gameObject);
    }

}