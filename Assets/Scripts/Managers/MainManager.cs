using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : Manager
{
    public ComponentManager componentManager { get; private set; }
    public SelectionManager selectionManager { get; private set; }
    public ClipboardManager clipboardManager { get; private set; }

    private void Awake()
    {
        selectionManager = gameObject.AddComponent<SelectionManager>();
        componentManager = gameObject.AddComponent<ComponentManager>();
        clipboardManager = Instantiate(((GameObject)Resources.Load("Clipboard"))).GetComponent<ClipboardManager>();
    }

    // TODO: add public (generic?) methods to enable and disable managers.
    // public void EnableManager<T>() {

    // }

    // public void DisableManager<T>() {

    // }
}
