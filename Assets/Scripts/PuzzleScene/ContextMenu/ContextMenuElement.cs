using System;

public class ContextMenuElement{
    public string Text{get; private set;}
    public string Shortcut{get; private set;}
    public Action ClickAction{get; set;}

    public ContextMenuElement(string text, string shortcut){
        Text = text;
        Shortcut = shortcut;
    }
}