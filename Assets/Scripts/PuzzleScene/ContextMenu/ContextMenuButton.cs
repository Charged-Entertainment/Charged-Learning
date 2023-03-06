using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuButton : MonoBehaviour
{
    [SerializeField] private Text textMessage;
    [SerializeField] private Text shortcut;
    private ContextMenuElement menuElement;

    public ContextMenuElement MenuElement{
        get{
            return menuElement;
        }
        set{
            menuElement = value;
            textMessage.text = menuElement.Text;
            shortcut.text = menuElement.Shortcut;
        }
    }

    void Awake()
    {
        textMessage = transform.Find("Text").GetComponent<Text>();
        shortcut = transform.Find("Shortcut").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnClick(){
        Debug.Log($"OnClick on: {MenuElement.ClickAction}");
        MenuElement.ClickAction?.Invoke();
    }
    
}
