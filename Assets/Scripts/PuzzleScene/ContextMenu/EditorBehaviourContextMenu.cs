using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EditorBehaviourContextMenu : ContextMenu
{
    public ContextMenuElement FlipHorizontalElement { get; private set; }
    public ContextMenuElement FlipVerticalElement { get; private set; }
    public ContextMenuElement RotateRightElement { get; private set; }
    public ContextMenuElement RotateLeftElement { get; private set; }
    public ContextMenuElement DeleteElement { get; private set; }
    public ContextMenuElement CopyElement { get; private set; }
    public ContextMenuElement CutElement { get; private set; }
    public ContextMenuElement PasteElement { get; private set; }
    protected new void Awake()
    {
        FlipHorizontalElement = new ContextMenuElement("Flip Horizontally", "Shift+H");
        FlipVerticalElement = new ContextMenuElement("Flip Vertically", "Shift+V");
        RotateRightElement = new ContextMenuElement("Rotate right", "Ctrl+R");
        RotateLeftElement = new ContextMenuElement("Rotate left", "Ctrl+Q");
        DeleteElement = new ContextMenuElement("Delete", "Delete");
        CopyElement = new ContextMenuElement("Copy", "Ctrl+C");
        CutElement = new ContextMenuElement("Cut", "Ctrl+X");
        PasteElement = new ContextMenuElement("Paste", "Ctrl+V");

        contextMenuElements = new List<ContextMenuElement>()
        {
            FlipHorizontalElement,FlipVerticalElement,
            RotateLeftElement,RotateRightElement,DeleteElement, CopyElement, CutElement, PasteElement
        };

        foreach(var editorBehaviour in Selection.GetSelectedComponents<EditorBehaviour>()){
            SubscribeToAction(editorBehaviour);
        }

        CopyElement.ClickAction = Clipboard.Copy;
        PasteElement.ClickAction = Clipboard.Paste;
        CutElement.ClickAction = Clipboard.Cut;

        base.Awake();
    }

    public void SubscribeToAction(MonoBehaviour monoBehaviour){
        var editorBehaviour = monoBehaviour as EditorBehaviour;
        if(editorBehaviour == null) return;
        
        FlipHorizontalElement.ClickAction += editorBehaviour.FlipH;
        FlipVerticalElement.ClickAction += editorBehaviour.FlipV;
        RotateRightElement.ClickAction += () => editorBehaviour.Rotate(-90);
        RotateLeftElement.ClickAction += () => editorBehaviour.Rotate(90);
        DeleteElement.ClickAction += editorBehaviour.Destroy;
    }
}