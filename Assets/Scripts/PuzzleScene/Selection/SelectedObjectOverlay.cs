using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SelectedObjectOverlay : MonoBehaviour
{
    GameObject go;
    private void Awake() {
        var prefab = Resources.Load<GameObject>("Prefabs/SelectedObjectOverlay");
        go = GameObject.Instantiate(prefab, transform);        
        var oldRotation = transform.eulerAngles;
        transform.eulerAngles = Vector3.zero;
        go.transform.localScale = transform.GetComponent<SpriteRenderer>().bounds.extents*2;
        transform.eulerAngles = oldRotation;
    }

    private void OnDestroy() {
        GameObject.Destroy(go);
    }
}
