using System;

public class ContextMenuElement{
    public string Text{get; private set;}
    public string Shortcut{get; private set;}
    public Action ClickAction{get; private set;}

    public ContextMenuElement(string text, string shortcut, Action clickAction){
        Text = text;
        Shortcut = shortcut;
        ClickAction = clickAction;
    }
}