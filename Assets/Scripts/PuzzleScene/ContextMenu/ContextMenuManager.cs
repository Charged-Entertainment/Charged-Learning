using UnityEngine;
using UnityEngine.UI;

public class ContextMenuManager : Singleton<ContextMenuManager>
{
    [SerializeField] static private ContextMenu contextMenu = null;

    private void Start()
    {

    }

    public static void CreateMenu(ContextMenuObject contextMenuObject, Vector2 position)
    {
        if(contextMenu == null) contextMenu = Instantiate(((GameObject)Resources.Load("Prefabs/ContextMenu/ContextMenu"))).GetComponent<ContextMenu>();
        Debug.Log($"ContextMenu: {contextMenu}");
        contextMenu.transform.position = position;
        contextMenu.AddElements(contextMenuObject.GetContextMenuElements());

    }

}