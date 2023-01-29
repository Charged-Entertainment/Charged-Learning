using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : Manager
{
    public ComponentManager componentManager { get; private set; }
    public SelectionManager selectionManager { get; private set; }
    public ClipboardManager clipboardManager { get; private set; }

    protected override void Awake()
    {
        selectionManager = gameObject.AddComponent<SelectionManager>();
        componentManager = gameObject.AddComponent<ComponentManager>();
        clipboardManager = Instantiate(((GameObject)Resources.Load("Clipboard"))).GetComponent<ClipboardManager>();
    }

    public void EnableManager(Manager manager) {
        manager.Enable();
    }

    public void DisableManager(Manager manager) {
        manager.Disable();
    }
}
