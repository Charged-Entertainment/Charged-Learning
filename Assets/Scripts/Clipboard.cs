using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    public List<ComponentBehavior> clipboard { get; private set; }
    private void Awake()
    {
        clipboard = new List<ComponentBehavior>();
    }

    public void Copy(ComponentBehavior[] components, bool isCut = false)
    {
        Debug.Log("Copying!");
        clipboard.Clear();

        foreach (var component in components)
        {
            ComponentBehavior copy = Instantiate(component, transform);
            copy.gameObject.SetActive(false);

            clipboard.Add(copy);
            if (isCut) GameObject.Destroy(component);
        }
    }

    public void Paste(Vector3 pos)
    {
        Debug.Log($"Pasting at {pos}");
        transform.position = pos;

        foreach (var component in clipboard)
        {
            component.gameObject.SetActive(true);
            component.gameObject.transform.parent = null;
            component.gameObject.transform.position = new Vector3(component.gameObject.transform.position.x, component.gameObject.transform.position.y, 0); 
        }

        transform.position = Vector3.zero;
    }
}
