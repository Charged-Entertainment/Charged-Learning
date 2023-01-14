using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardManager : Manager
{
    public void Copy(ComponentBehavior[] components, bool isCut = false)
    {
        Debug.Log("Copying!");
        Clear();
        Bounds bounds = Utils.GetBoundsOfComponentBehaviorArray(components);

        transform.position = bounds.center;

        foreach (var component in components)
        {
            ComponentBehavior copy = mainManager.componentManager.ComponentInstantiate(component, transform);
            copy.transform.position -= transform.position;
            copy.Disable();

            if (isCut) GameObject.Destroy(component);
        }
    }

    public void Paste(Vector2 pos)
    {
        Debug.Log($"Pasting at {pos}");
        transform.position = pos;

        foreach (var component in GetContent())
        {
            component.Enable();
            component.gameObject.transform.parent = null;
        }

        transform.position = Vector3.zero;
    }

    public ComponentBehavior[] GetContent() {
        return GetComponentsInChildren<ComponentBehavior>(true);
    }

    public void Clear() {
        foreach (var component in GetContent())
        {
            component.Destroy();
        }
    }
}
