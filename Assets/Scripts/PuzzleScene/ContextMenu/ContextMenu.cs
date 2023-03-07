using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        content = GameObject.Find("Content").transform;
        buttonPrefab = (GameObject)Resources.Load("Prefabs/ContextMenu/ContextMenuButton");
        Debug.Log($"buttonPrefab: {buttonPrefab}");
    }

    public void AddElements(List<ContextMenuElement> elements)
    {
        foreach (var element in elements)
        {
            Debug.Log("add Element");
            var contextMenuButton = Instantiate(buttonPrefab, content).GetComponent<ContextMenuButton>();
            contextMenuButton.transform.localScale = Vector3.one;
            Debug.Log($"ContextMenuButton parent: {contextMenuButton.transform.parent}");
            contextMenuButton.MenuElement = element;
        }
    }


}