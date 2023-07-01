using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class ContextMenu : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] protected List<ContextMenuElement> contextMenuElements;

    protected void Awake()
    {
        content = GameObject.Find("Content").transform;
        buttonPrefab = (GameObject)Resources.Load("Prefabs/ContextMenu/ContextMenuButton");
        AddElements(contextMenuElements);
    }

    protected void AddElements(List<ContextMenuElement> elements)
    {
        foreach (var element in elements)
        {
            var contextMenuButton = Instantiate(buttonPrefab, content).GetComponent<ContextMenuButton>();
            contextMenuButton.transform.localScale = Vector3.one;
            contextMenuButton.MenuElement = element;
        }
    }


}