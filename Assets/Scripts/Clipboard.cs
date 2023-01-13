using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    public void Copy(ComponentBehavior[] components, bool isCut = false)
    {
        Debug.Log("Copying!");
        Clear();

        foreach (var component in components)
        {
            ComponentBehavior copy = Instantiate(component, transform);
            copy.gameObject.SetActive(false);

            if (isCut) GameObject.Destroy(component);
        }
    }

    public void Paste(Vector2 pos)
    {
        Debug.Log($"Pasting at {pos}");
        transform.position = pos;

        foreach (var component in GetContent())
        {
            component.gameObject.SetActive(true);
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
